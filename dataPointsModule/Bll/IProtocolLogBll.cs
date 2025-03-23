using domain.Pojo.protocol;
using domain.Records;
using infrastructure.Utils;

namespace dataPointsModule.Bll;

public interface IProtocolLogBll
{

    
    void Save(ProtocolLog protocolLog);

    void Delete(List<long> ids);

    Pager<ProtocolLog> GetList(ProtocolLogQuery query);
    
    


}