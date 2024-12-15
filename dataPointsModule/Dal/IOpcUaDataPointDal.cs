using domain.Enums;
using domain.Pojo.protocol;
using domain.Records;
using infrastructure.Utils;

namespace dataPointsModule.Dal;

public interface IOpcUaDataPointDal
{
    
    void DeleteBatchById(List<long> ids);
    void Insert(OpcUaDataPoint opcUaDataPoint);
    void Update(OpcUaDataPoint opcUaDataPoint);
    
    void DeleteById(long opcUaDataPointId);
    OpcUaDataPoint SelectById(long opcUaDataPointId);
    OpcUaDataPoint SelectByNameAndOperate(string name, OperateEnum operate);
    
    Pager<OpcUaDataPoint> SelectList(OpcUaDataPointQuery query);
    
}