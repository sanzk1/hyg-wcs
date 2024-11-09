using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace jobcoreModule.Core;
using System.Collections.Concurrent;
using System.Diagnostics;
using infrastructure.Attributes;
using Microsoft.Extensions.DependencyInjection;


/// <summary>
/// 任务核心控制，控制作业线程暂停、恢复、终止
/// </summary>
[Component(ServiceLifetime.Singleton)]
public class JobCoreContext
{
    /// <summary>
    /// key-任务id，value-同步信号量
    /// </summary>
    public ConcurrentDictionary<string, ManualResetEvent> taskPool = new();
    /// <summary>
    /// key-任务id，value-取消信号量
    /// </summary>
    public ConcurrentDictionary<string, CancellationTokenSource > cancelPool = new();

    private readonly ILogger<JobCoreContext> _logger;

    public JobCoreContext(ILogger<JobCoreContext> logger)
    {
        this._logger = logger;
    }
    
    /// <summary>
    /// 根据任务id创建新的线程是否阻塞信号
    /// </summary>
    /// <param name="taskId"></param>
    /// <returns></returns>
    private ManualResetEvent GetNewManualResetEvent(string taskId)
    {
        ManualResetEvent resetEvent = new ManualResetEvent(false);
        taskPool.TryAdd(taskId, resetEvent);
        return resetEvent;
    }
    /// <summary>
    /// 根据任务id产生新的任务线程取消信号
    /// </summary>
    /// <param name="taskId"></param>
    /// <returns></returns>
    public CancellationToken  GetCancellationToken(string taskId)
    {
        if (GetNewManualResetEvent(taskId) == null)
        {
            throw new Exception("创建线程同步信号量失败");
        }
        CancellationTokenSource  cancellation = new CancellationTokenSource();
        cancellation.Token.Register(() => _logger.LogWarning($"{taskId} - 任务已放弃"));
        cancelPool.TryAdd(taskId, cancellation);
        return cancellation.Token;
    }
    
    /// <summary>
    /// 通知任务线程暂停
    /// </summary>
    /// <param name="taskId"></param>
    public void PuseTask(string taskId)
    {
        if (taskPool.TryGetValue(taskId, out ManualResetEvent manualResetEvent))
        {
            _logger.LogInformation($"{Thread.CurrentThread.Name}-线程继续暂停");
            manualResetEvent.Reset(); 
        }
        
    }
    
    /// <summary>
    /// 通知任务线程继续
    /// </summary>
    /// <param name="taskId"></param>
    public void SetTask(string taskId)
    {
        if (taskPool.TryGetValue(taskId, out ManualResetEvent manualResetEvent))
        {
            _logger.LogInformation(Thread.CurrentThread.Name + "-线程继续执行");
            manualResetEvent.Set();
        }
        
    }

    /// <summary>
    /// 取消任务线程
    /// </summary>
    /// <param name="taskId"></param>
    public void CancelTask(string taskId)
    {
        if (cancelPool.TryGetValue(taskId, out CancellationTokenSource  cancellationTokenSource))
        {
            _logger.LogInformation(Thread.CurrentThread.ManagedThreadId + "线程取消执行" + taskId);
            cancellationTokenSource.CancelAfter(1000); 
        }
        
    }

    /// <summary>
    /// 根据信号控制线程执行状态
    /// </summary>
    /// <param name="taskId"></param>
    public void WaitOne(string taskId)
    {
        if (taskPool.TryGetValue(taskId, out ManualResetEvent manualResetEvent))
        {
            manualResetEvent.WaitOne();
        }
    }

    /// <summary>
    /// 根据取消信号取消当前线程
    /// </summary>
    /// <param name="taskId"></param>
    public void IsStopped(string taskId)
    {
        if (cancelPool.TryGetValue(taskId, out CancellationTokenSource  cancellationTokenSource)
            && cancellationTokenSource.IsCancellationRequested)
        {
            Thread.CurrentThread.Interrupt();
        }
    }
    public ManualResetEvent GetManualResetEvent(string taskId)
    {
        taskPool.TryGetValue(taskId, out ManualResetEvent manualResetEvent);
        return manualResetEvent;
    }
    public CancellationTokenSource GetCancellationTokenSource(string taskId)
    {
        cancelPool.TryGetValue(taskId, out CancellationTokenSource cancellationTokenSource);
        return cancellationTokenSource;
    }

    /// <summary>
    /// 线程结束移除线程信号量
    /// </summary>
    /// <param name="threadName"></param>
    public void RemoveThread(string threadName)
    {
            cancelPool.Remove(threadName, out CancellationTokenSource cancellationTokenSource);
            taskPool.Remove(threadName, out ManualResetEvent manualResetEvent);
            manualResetEvent.Dispose();
            cancellationTokenSource.Dispose();
    }
    

    /// <summary>
    /// 监听当前任务线程状态
    /// </summary>
    /// <param name="threadName"></param>
    public void Monitor(string threadName)
    {
            WaitOne(threadName);
            IsStopped(threadName);
    }
    
}