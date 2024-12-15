using api.Common.DTO;
using domain.Pojo.protocol;
using domain.Records;
using infrastructure.Utils;

namespace dataPointsModule.Bll;

public interface IOpcUaDataPointBll
{

    public void Save(OpcUaDataPoint point);
    
    public DataPointDto ReadByName(string name);
    public DataPointDto ReadById(long id);
    public DataPointDto WriteByName(string name, object value);
    public DataPointDto WriteByName(long id, object value);
    
    public void DeleteBatch(List<long> ids);

    public Pager<OpcUaDataPoint> GetList(OpcUaDataPointQuery query);
    

}