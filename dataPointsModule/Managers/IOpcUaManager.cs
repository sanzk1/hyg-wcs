using domain.Dto;
using domain.Pojo.protocol;

namespace dataPointsModule.Managers;

public interface IOpcUaManager
{
    
    public string[] accessTypes { get; }

    public DataPointDto Read(OpcUaDataPoint point);  
    
    public DataPointDto Write(OpcUaDataPoint point, object obj);  
    
    public void Connect(OpcUaDataPoint point);

    
    
}