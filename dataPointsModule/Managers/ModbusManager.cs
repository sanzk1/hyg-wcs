using System.Collections.Concurrent;
using System.Net;
using api.Common.DTO;
using dataPointsModule.Attributes;
using dataPointsModule.Managers.Base;
using domain.Enums;
using domain.Pojo.protocol;
using infrastructure.Attributes;
using IoTClient.Clients.Modbus;
using IoTClient.Enums;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace dataPointsModule.Managers;

/// <summary>
/// Modbus长连接管理
/// </summary>
[Component(ServiceLifetime.Singleton)]
public class ModbusManager : ManagerAbstract<ModbusDataPoint>, IModbusManager
{
    public readonly ConcurrentDictionary<string, ModbusTcpClient> ModbusOnline = new();
    private readonly ILogger<ModbusManager> logger;
    private object modbusLock = new();
    
    public ModbusManager(ILogger<ModbusManager> logger) 
    {
        this.logger = logger; 
    }

    public override void Connect(ModbusDataPoint t)
    {
        try
        {
            ModbusTcpClient client = new ModbusTcpClient(new IPEndPoint(IPAddress.Parse(t.ip), t.port), 1500,
                getEndianFormat(t.format));
            client.Open();
            if (client.Connected)
            { 
                bool b = ModbusOnline.TryAdd(getKey(t), client);
                if (b)
                    logger.LogInformation($"Modbus连接成功，IP：{t.ip} port:{t.port}");
                else
                {
                    client.Close();
                }
            }
        }
        catch (Exception ex)
        {
            logger.LogError($"Modbus连接失败，IP：{t.ip} port:{t.port}，原因：{ex.Message}");
        }
    }

    private EndianFormat getEndianFormat(string format)
    {
        return (EndianFormat)Enum.Parse(typeof(EndianFormat), format);
    }

    public override void Disconnect(ModbusDataPoint t)
    {
        try
        {
            bool b = ModbusOnline.Remove(getKey(t), out ModbusTcpClient client);
            if (b)
            {
                client.Close();
                logger.LogInformation($"Modbus断开连接成功，IP：{t.ip} port:{t.port}");
            }  
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public override void Reconnect(ModbusDataPoint t)
    {
        try
        {
            logger.LogInformation($"Modbus正在重新连接，IP：{t.ip} port:{t.port}");

            bool b = ModbusOnline.TryGetValue(getKey(t), out ModbusTcpClient client);
            if (b)
            {
                Disconnect(t);
                return;
            }
            Connect(t);
        }
        catch (Exception ex) 
        {
            logger.LogInformation($"Modbus正在重新连接异常，IP：{t.ip} port:{t.port}，原因：{ex.Message}");
        }
    }

    public override object GetDevice(ModbusDataPoint t)
    {
        try
        {
            lock (modbusLock)
            {
                bool b = ModbusOnline.TryGetValue(getKey(t), out ModbusTcpClient client);
                if (b && client.Connected)
                {
                    return client;
                }
                Reconnect(t);
                return null;
            }  
        }
        catch (Exception ex)
        {
            logger.LogError($"Modbus连接获取失败，IP：{t.ip} port:{t.port}，原因：{ex.Message}");
        }
        return null;
    }

    [ProtocolLog(OperateEnum.Read, ProtocolEnum.ModbusTcp)]
    public DataPointDto Read(ModbusDataPoint t)
    {
        ModbusTcpClient client = GetDevice(t) as ModbusTcpClient;
        if (client == null)
        {
            return new DataPointDto(false, null, $"Modbus设备不在线，IP：{t.ip} port:{t.port}");
        }
        object? res = null;
        try
        {

            return new DataPointDto();
        }
        catch (Exception ex)
        {
            return new DataPointDto(false, res, ex.Message);
        }
        
    }

    public DataPointDto Write(ModbusDataPoint t)
    {
        throw new NotImplementedException();
    }

    private string getKey(ModbusDataPoint t)
    {
        return $"{t.ip}:{t.port}";
    }


}