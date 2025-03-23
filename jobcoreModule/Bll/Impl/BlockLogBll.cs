using domain.Pojo.jobCore;
using infrastructure.Attributes;
using infrastructure.Db;
using infrastructure.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Yitter.IdGenerator;

namespace jobcoreModule.Bll.Impl;


[Service]
public class BlockLogBll : IBlockLogBll
{
    
    private readonly ILogger<TaskInfoBll> _logger;
    private readonly DbClientFactory _dbClientFactory = ServiceUtil.GetRequiredService<DbClientFactory>() ;

    public BlockLogBll(ILogger<TaskInfoBll> logger)
    {
        this._logger = logger;
    }
    
    public void Save(BlockLog log)
    {
        using var db = _dbClientFactory.GetSqlSugarClient();
        db.Insertable<BlockLog>(log).ExecuteCommand();
    }

    public void Update(BlockLog log)
    {
        using var db = _dbClientFactory.GetSqlSugarClient();
        db.Updateable(log).ExecuteCommand();
    }

    public BlockLog GetByTaskIdAndBId(string taskId, string bId)
    {
        using var db = _dbClientFactory.GetSqlSugarClient();
        return db.Queryable<BlockLog>().First(x => x.taskId.Equals(taskId) && x.bId.Equals(bId));
    }
}