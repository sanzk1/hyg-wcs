using domain.Pojo.ortherSystems;
using infrastructure.Attributes;
using infrastructure.Db;
using infrastructure.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace otherSystemModule.Dal.Impl;


[Service(ServiceLifetime.Singleton)]
public class InterfaceRequestConfigDal : IInterfaceRequestConfigDal
{
    private DbClientFactory dbClientFactory => ServiceUtil.GetRequiredService<DbClientFactory>();


    public void Insert(InterfaceRequestConfig config)
    {
        using var db = dbClientFactory.GetSqlSugarClient();
        db.Insertable(config).ExecuteCommand();
    }

    public void Update(InterfaceRequestConfig config)
    {
        using var db = dbClientFactory.GetSqlSugarClient();
        db.Updateable(config).ExecuteCommand();
    }

    public void Delete(InterfaceRequestConfig config)
    {
        using var db = dbClientFactory.GetSqlSugarClient();
        db.Deleteable(config).ExecuteCommand();
    }

    public void DeleteById(List<long> ids)
    {
        if (ids.Count == 0)
        {
            return;
        }
        using var db = dbClientFactory.GetSqlSugarClient();
        db.Deleteable<InterfaceRequestConfig>().In(ids).ExecuteCommand();
    }

    public InterfaceRequestConfig SelectById(long id)
    {
        using var db = dbClientFactory.GetSqlSugarClient();
        return db.Queryable<InterfaceRequestConfig>().Single(i => i.configId == id && i.isDelete == false);
    }
}