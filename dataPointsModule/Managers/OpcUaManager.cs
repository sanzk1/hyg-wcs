using System.Collections.Concurrent;
using System.Net;
using api.Common.DTO;
using dataPointsModule.Attributes;
using dataPointsModule.Managers.Base;
using domain.Enums;
using domain.Pojo.protocol;
using infrastructure.Attributes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Workstation.ServiceModel.Ua;
using Workstation.ServiceModel.Ua.Channels;

namespace dataPointsModule.Managers;

/// <summary>
/// opc ua 长连接
/// </summary>
[Service(ServiceLifetime.Singleton)]
public class OpcUaManager : ManagerAbstract<OpcUaDataPoint>, IOpcUaManager
{ public string[] accessTypes { get; } = { "s", "i", "g" };
    public readonly ConcurrentDictionary<string, ClientSessionChannel> ClientSessionChannels = new ();
    private readonly ILogger<OpcUaManager> logger;
    private object opcUaLock = new();
    

    public OpcUaManager(ILogger<OpcUaManager> logger)
    {
        this.logger = logger;
    }
    
    public override void Connect(OpcUaDataPoint t)
    {
        ClientSessionChannel channel;
        try
        {
            channel = new ClientSessionChannel(
                GetApplicationDescription(),
                null,
                new AnonymousIdentity(),
                t.endpoint,
                SecurityPolicyUris.None);
             channel.OpenAsync().Wait();
            ClientSessionChannels.TryAdd(t.endpoint, channel);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"OpcUa协议plc IP:{GetIp(t.endpoint)} 连接失败：{ex.Message}");
        }

    }

    public override void Disconnect(OpcUaDataPoint t)
    {
        try
        {
            bool b = ClientSessionChannels.TryGetValue(t.endpoint, out var channel);
            if (b)
            {
                ClientSessionChannels.TryRemove(t.endpoint, out channel);
                channel.CloseAsync().Wait();
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"OpcUa协议plc IP:{GetIp(t.endpoint)} 断开连接失败：{ex.Message}");
        }
    }

    public override void Reconnect(OpcUaDataPoint t)
    {
        try
        {
            logger.LogInformation($"OpcUa协议plc正在重新连, IP:{GetIp(t.endpoint)}");

            bool b = ClientSessionChannels.TryGetValue(t.endpoint, out ClientSessionChannel channel);
            if (b)
            {
                Disconnect(t);
                return;
            }
            Connect(t);
        }
        catch (Exception ex) 
        {
            logger.LogError($"OpcUa协议plc正在重新连接异常，IP：{GetIp(t.endpoint)}，异常原因：{ex.Message}");
        }
    }

    public override object GetDevice(OpcUaDataPoint t)
    {
        try
        {
            lock (opcUaLock)
            {
                bool b = ClientSessionChannels.TryGetValue(t.endpoint, out ClientSessionChannel plc);
                if (b && plc.State == CommunicationState.Opened)
                {
                    return plc;
                }
                Reconnect(t);
                return null;
            }  
        }
        catch (Exception ex)
        {
            logger.LogError($"OpcUa协议在线plc连接获取失败，原因：{ex.Message}");
        }
        return null;
    }

    private string GetIp(string endpoint)
    {
        return endpoint.Split(":")[1].Replace("//", "");
    }

    private ApplicationDescription GetApplicationDescription()
    {
        var clientDescription = new ApplicationDescription
        {
            ApplicationName = "Hygge.UaClient",
            ApplicationUri = $"urn:{Dns.GetHostName()}:Hygge.UaClient",
            ApplicationType = ApplicationType.Client
        };
        return clientDescription;
    }
    
    [ProtocolLog(OperateEnum.Read, ProtocolEnum.OpcUa)]
    public DataPointDto Read(OpcUaDataPoint point)
    {
        ClientSessionChannel? plc  = GetDevice(point) as ClientSessionChannel;
        if (plc == null)
        {
            return new DataPointDto(false, null, $"OpcUa协议plc不在线，IP：{GetIp(point.endpoint)}");
        }
        ReadRequest request = new ReadRequest {
            NodesToRead = new[] {
                new ReadValueId {
                    NodeId = NodeId.Parse(GetNodeId(point)),
                    AttributeId = AttributeIds.Value
                }
            }
        };
        var result =  plc.ReadAsync(request).GetAwaiter().GetResult();
        DataValue dataValue = result.Results[0];
        bool b = false;
        if (dataValue.StatusCode.Value == 0)
            b = true;
        return new DataPointDto(b, dataValue.Value, dataValue.StatusCode.Value.ToString());
    }

    [ProtocolLog(OperateEnum.Write, ProtocolEnum.OpcUa)]
    public DataPointDto Write(OpcUaDataPoint point, object value)
    {
        ClientSessionChannel? plc  = GetDevice(point) as ClientSessionChannel;
        if (plc == null)
        {
            return new DataPointDto(false, null, $"OpcUa协议plc不在线，IP：{GetIp(point.endpoint)}");
        }
        WriteRequest request = new WriteRequest() {
            NodesToWrite = new[] {
                new WriteValue() {
                    NodeId = NodeId.Parse(GetNodeId(point)),
                    AttributeId = AttributeIds.Value,
                    Value = new DataValue(value)
                },                  
            }
        };
        var result = plc.WriteAsync(request).GetAwaiter().GetResult();
        StatusCode code = result.Results[0];
        bool b = false;
        if (code.Value == 0)
            b = true;
        return new DataPointDto(b, value, code.Value.ToString());
    }

    private string GetNodeId(OpcUaDataPoint point)
    {
        return $"ns={point.namespaceIndex};{point.accessType}={point.identifier}";
    }

  

    

}