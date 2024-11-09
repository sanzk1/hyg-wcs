using System;
using System.Threading;
using System.Threading.Tasks;
using AspectCore.DependencyInjection;
using AspectCore.DynamicProxy;
using infrastructure.Utils;
using jobcoreModule.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace jobcoreModule.Attributes;


/// <summary>
/// 监听当前线程状态，根据任务状态进行控制
/// </summary>
[AttributeUsage(AttributeTargets.Method  | AttributeTargets.Class)]
public class MonitoringAttribute: AbstractInterceptorAttribute
{
    [FromServiceContext]
    public ILogger<MonitoringAttribute> logger { get; set; }
    [FromServiceContext]
    public JobCoreContext jobCoreContext { set; get; }
    
    public override async Task Invoke(AspectContext context, AspectDelegate next)
    {
        // JobCoreContext jobCoreContext = ServiceUtil.ServiceProvider.GetRequiredService<JobCoreContext>();
        // 监听状态
        if (!string.IsNullOrEmpty(Thread.CurrentThread.Name))
        {
            jobCoreContext.Monitor(Thread.CurrentThread.Name);
        }
        
        await next(context);
        
        // 监听状态
        if (!string.IsNullOrEmpty(Thread.CurrentThread.Name))
        {
            jobCoreContext.Monitor(Thread.CurrentThread.Name);
        }
    }
    
}