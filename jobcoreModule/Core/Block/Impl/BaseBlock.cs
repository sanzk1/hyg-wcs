using domain.Pojo.jobCore;
using infrastructure.Attributes;
using jobcoreModule.Attributes;
using jobcoreModule.Bll;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace jobcoreModule.Core.Block.Impl;


[Service(ServiceLifetime.Singleton)]
public class BaseBlock : IBaseBlock
{
    private readonly ILogger<BaseBlock> logger;
    private readonly IBlockLogBll blockLogBll;

    public BaseBlock(ILogger<BaseBlock> logger, IBlockLogBll blockLogBll)
    {
        this.logger = logger;
        this.blockLogBll = blockLogBll;
    }


    [Monitoring]
    [BlockLog]
    public void Print(string id, string value)
    {
        BlockLog log = blockLogBll.GetByTaskIdAndBId(Thread.CurrentThread.Name, id);
        log.txt = $">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>打印内容：{value}";
        blockLogBll.Update(log);
    }
    
    
}