using AspectCore.DynamicProxy;
using domain.Pojo.jobCore;
using infrastructure.Utils;
using jobcoreModule.Bll;
using Microsoft.Extensions.DependencyInjection;

namespace jobcoreModule.Attributes;

/// <summary>
/// 记录动作块日志
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class BlockLogAttribute: AbstractInterceptorAttribute
{
    private IBlockLogBll blockLogBll = ServiceUtil.ServiceProvider.GetRequiredService<IBlockLogBll>();
    
    public override async Task Invoke(AspectContext context, AspectDelegate next)
    {
        var Paramters = context.Parameters;
        BlockLog log = new();
        log.taskId = Thread.CurrentThread.Name;
        log.bId = Paramters[0].ToString();
        blockLogBll.Save(log);
        try
        {
            await next(context);
            BlockLog newLog = blockLogBll.GetByTaskIdAndBId(log.taskId, log.bId);
            newLog.updateTIme = DateTime.Now;
            newLog.executeStatus = true;
            blockLogBll.Update(newLog);
        }
        catch (ThreadInterruptedException e)
        {
            Thread.CurrentThread.Interrupt();
        }
        catch (Exception e)
        {
            BlockLog newLog = blockLogBll.GetByTaskIdAndBId(log.taskId, log.bId);
            newLog.txt = $"{newLog.txt} \\n \\t {e.Message}";
            newLog.executeStatus = false;
            newLog.updateTIme = DateTime.Now;
            blockLogBll.Update(newLog);
        }

    }
    
}