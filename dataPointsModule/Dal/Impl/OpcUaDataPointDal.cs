using domain.Enums;
using domain.Pojo.protocol;
using domain.Records;
using infrastructure.Attributes;
using infrastructure.Db;
using infrastructure.Utils;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;

namespace dataPointsModule.Dal.Impl;


[Service(ServiceLifetime.Singleton)]
public class OpcUaDataPointDal : IOpcUaDataPointDal
{
    private DbClientFactory dbClientFactory => ServiceUtil.GetRequiredService<DbClientFactory>();


    public void DeleteBatchById(List<long> ids)
    {
        using var db = dbClientFactory.GetSqlSugarClient();
        db.Deleteable<OpcUaDataPoint>().In(ids).ExecuteCommand();
    }

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

    public OpcUaDataPoint SelectById(long opcUaDataPointId)
    {
        using var db = dbClientFactory.GetSqlSugarClient();
        return db.Queryable<OpcUaDataPoint>().Single(o => o.id == opcUaDataPointId);
    }

    public OpcUaDataPoint SelectByNameAndOperate(string name, OperateEnum operate)
    {
        using var db = dbClientFactory.GetSqlSugarClient();
        return db.Queryable<OpcUaDataPoint>().Where(o => o.name.Equals(name) && o.operate == operate).Single();
    }

    public Pager<OpcUaDataPoint> SelectList(OpcUaDataPointQuery query)
    {
        using var db = dbClientFactory.GetSqlSugarClient();
        var exp = Expressionable.Create<OpcUaDataPoint>();
        exp.AndIF(!string.IsNullOrEmpty(query.name), e => e.name.Contains(query.name));
        exp.AndIF(!string.IsNullOrEmpty(query.category), e => e.name.Contains(query.category));
        exp.AndIF(!string.IsNullOrEmpty(query.identifier), e => e.name.Contains(query.identifier));
        exp.AndIF(!string.IsNullOrEmpty(query.ip), e => e.name.Contains(query.ip));

        Pager<OpcUaDataPoint> pager = new(query.pageNum, query.pageSize);
        pager.rows = db.Queryable<OpcUaDataPoint>().Where(exp.ToExpression())
            .Skip(pager.getSkip()).Take(pager.pageSize)
            .ToList();
        pager.total = db.Queryable<OpcUaDataPoint>().Where(exp.ToExpression()).Count();
        
        return pager;;
    }
}