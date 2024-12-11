using domain.Pojo.sys;
using infrastructure.Attributes;
using infrastructure.Db;
using Microsoft.Extensions.DependencyInjection;

namespace adminModule.Dal.Impl;

[Service(ServiceLifetime.Singleton)]
public class SysFileInfoDal : ISysFileInfoDal
{

    private readonly DbClientFactory _dbClientFactory;
    public SysFileInfoDal(DbClientFactory dbClientFactory)
    {
        _dbClientFactory = dbClientFactory;
    }
    
    
    public void Insert(SysFileInfo entity)
    {
        using var db = _dbClientFactory.GetSqlSugarClient();
        db.Insertable<SysFileInfo>(entity).ExecuteCommand();
    }
    
    
}