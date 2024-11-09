using api.Common.DTO;
using dataPointsModule.Managers.Base;
using domain.Pojo.protocol;
using infrastructure.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace dataPointsModule.Managers;

/// <summary>
/// Modbus长连接管理
/// </summary>
[Component(ServiceLifetime.Singleton)]
public class ModbusManager : ManagerAbstract<ModbusDataPoint>, IModbusManager
{
    

    public override void Connect(ModbusDataPoint t)
    {
        throw new NotImplementedException();
    }

    public override void Disconnect(ModbusDataPoint t)
    {
        throw new NotImplementedException();
    }

    public override void Reconnect(ModbusDataPoint t)
    {
        throw new NotImplementedException();
    }

    public override object GetDevice(ModbusDataPoint t)
    {
        throw new NotImplementedException();
    }

    public DataPointDto Read(ModbusDataPoint t)
    {
        throw new NotImplementedException();
    }

    public DataPointDto Write(ModbusDataPoint t)
    {
        throw new NotImplementedException();
    }


}