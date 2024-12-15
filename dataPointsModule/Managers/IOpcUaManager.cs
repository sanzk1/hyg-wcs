using api.Common.DTO;
using domain.Pojo.protocol;

namespace dataPointsModule.Managers;

public interface IOpcUaManager
{
    
    
    public DataPointDto Read(OpcUaDataPoint point);  
    
    public DataPointDto Write(OpcUaDataPoint point, object obj);  
    
    public void Connect(OpcUaDataPoint point);

    
    
}