using domain.Pojo.protocol;
using domain.Records;
using infrastructure.Attributes;
using infrastructure.Db;
using infrastructure.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SqlSugar;

namespace dataPointsModule.Bll.Impl;

[Service(ServiceLifetime.Singleton)]
public class ProtocolLogBll : IProtocolLogBll
{
    private readonly ILogger<ProtocolLogBll> _logger;
    private readonly DbClientFactory _dbClientFactory;

    public ProtocolLogBll(ILogger<ProtocolLogBll> logger, DbClientFactory dbClientFactory)
    {
        this._logger = logger;
        this._dbClientFactory = dbClientFactory;
    }

    public void Save(ProtocolLog protocolLog)
    {
        using var db = _dbClientFactory.GetSqlSugarClient();
        db.Insertable<ProtocolLog>(protocolLog).ExecuteCommand();
    }

    public void Delete(List<long> ids)
    {
        if (ids.Count == 0)
        {
            return;
        }
        using var db = _dbClientFactory.GetSqlSugarClient();
        db.Deleteable<ProtocolLog>().In(ids).ExecuteCommand();

    }

    public Pager<ProtocolLog> GetList(ProtocolLogQuery query)
    {
        Pager<ProtocolLog> pager = new(query.pageNum, query.pageSize);
        var exp = Expressionable.Create<ProtocolLog>();
        exp.AndIF(!string.IsNullOrEmpty(query.name), x => x.name.Contains(query.name));
        exp.AndIF(!string.IsNullOrEmpty(query.category), x => x.category.Equals(query.category));
        exp.AndIF(query.status != null, x => x.status == query.status);
        exp.AndIF(query.startTime != null, x => x.endTime >= query.startTime);
        exp.AndIF(query.endTime != null, x => x.endTime <= query.endTime);

        using var db = _dbClientFactory.GetSqlSugarClient();
        List<ProtocolLog> list = db.Queryable<ProtocolLog>().Where(exp.ToExpression())
            .OrderByDescending(x => x.endTime)
            .Skip(pager.getSkip()).Take(pager.pageSize)
            .ToList();
        pager.total = db.Queryable<ProtocolLog>().Where(exp.ToExpression()).Count();
        pager.rows = list;
        return pager;
    }
}