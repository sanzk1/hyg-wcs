using domain.Pojo.protocol;

namespace dataPointsModule.Dal;

public interface IOpcUaDataPointDal
{
    
    
    void Insert(OpcUaDataPoint opcUaDataPoint);
    void Update(OpcUaDataPoint opcUaDataPoint);
    
    void DeleteById(long opcUaDataPointId);
    
}