using domain.Pojo.protocol;
using infrastructure.Attributes;
using infrastructure.Db;
using infrastructure.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace dataPointsModule.Dal.Impl;


[Service(ServiceLifetime.Singleton)]
public class OpcUaDataPointDal : IOpcUaDataPointDal
{
    private DbClientFactory dbClientFactory => ServiceUtil.GetRequiredService<DbClientFactory>();


    public void Insert(OpcUaDataPoint opcUaDataPoint)
    {
        using var db = dbClientFactory.GetSqlSugarClient();
        db.Insertable(opcUaDataPoint).ExecuteCommand();
    }

    public void Update(OpcUaDataPoint opcUaDataPoint)
    {
        using var db = dbClientFactory.GetSqlSugarClient();
        db.Updateable(opcUaDataPoint).ExecuteCommand();
    }

    public void DeleteById(long opcUaDataPointId)
    {
        using var db = dbClientFactory.GetSqlSugarClient();
        db.Deleteable<OpcUaDataPoint>().Where( o => o.id == opcUaDataPointId).ExecuteCommand();
    }
}