using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using domain.Result;
using infrastructure.Utils;
using jobcoreModule.Core;
using jobcoreModule.TaskTemplate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace jobcoreModule.Controller;

[Route("[controller]/[action]")]
[ApiController]
public class TestJobCoreController : ControllerBase
{
    private JobCoreContext jobCoreContext;
    private ITaskTemplate taskTemplate;

    private ILogger<TestJobCoreController> _logger;

    public TestJobCoreController(JobCoreContext jobCoreContext, ILogger<TestJobCoreController> logger, ITaskTemplate taskTemplate)
    {
        this.jobCoreContext = jobCoreContext;
        this._logger = logger;
        this.taskTemplate = taskTemplate;
    }

    [HttpGet]
    public ApiResult newTask1(string taskId)
    {

        var c = jobCoreContext.GetCancellationToken(taskId);
        Task.Factory.StartNew((taskId1) =>
        {
            Thread.CurrentThread.Name = Convert.ToString(taskId1);
            jobCoreContext.WaitOne(taskId1.ToString());
            try
            {
                _logger.LogInformation($"{Thread.CurrentThread.Name}线程开始工作。。。");
              
                jobCoreContext.Monitor(Thread.CurrentThread.Name);
                _logger.LogInformation($"{Thread.CurrentThread.Name}-线程工作中工作工作。。。：");

               
                taskTemplate.test(taskId1.ToString());
                
                jobCoreContext.Monitor(Thread.CurrentThread.Name);
                Thread.Sleep(500);
              
            }
            catch (ThreadInterruptedException e)
            {
                _logger.LogError($"{Thread.CurrentThread.Name}-线程终止" + e.Message);
                jobCoreContext.RemoveThread(Thread.CurrentThread.Name);
            }
            catch (Exception exception)
            {
                _logger.LogError("线程异常终止：" + exception.Message);
                jobCoreContext.RemoveThread(Thread.CurrentThread.Name);
            }

        }, taskId, c);
        return ApiResult.succeed();
    }
    [HttpGet]
    public ApiResult newTask(string taskId)
    {
        
        var c = jobCoreContext.GetCancellationToken(taskId);
        Task.Factory.StartNew((taskId1) =>
        {
            Thread.CurrentThread.Name = Convert.ToString(taskId1);
            jobCoreContext.WaitOne(taskId);
            try
            {
                _logger.LogInformation($"{Thread.CurrentThread.Name}线程开始工作。。。");
                while (true)
                {
                    jobCoreContext.Monitor(Thread.CurrentThread.Name);
                    _logger.LogInformation($"{Thread.CurrentThread.Name}-线程工作中工作工作。。。：");
                    jobCoreContext.Monitor(Thread.CurrentThread.Name);
                    Thread.Sleep(500);
                }
            }
            catch (ThreadInterruptedException e)
            {
                _logger.LogError($"{Thread.CurrentThread.Name}-线程终止");
                jobCoreContext.RemoveThread(Thread.CurrentThread.Name);
            }
            catch (Exception exception)
            {
                _logger.LogError("线程异常终止");
            }

        }, taskId, c);
        
        return ApiResult.succeed();
    }

    [HttpGet]
    public ApiResult puse(string taskId)
    {
        jobCoreContext.PuseTask(taskId);
        return ApiResult.succeed();
    }
    
    [HttpGet]
    public ApiResult setTask(string taskId)
    {
        jobCoreContext.SetTask(taskId);
        return ApiResult.succeed();
    }
    [HttpGet]
    public ApiResult cancelTask(string taskId)
    {
        jobCoreContext.CancelTask(taskId);
        return ApiResult.succeed();
    }

    [HttpPost]
    public ApiResult clearTask()
    {
        foreach (var cancelPoolKey in jobCoreContext.cancelPool.Keys)
        {
            if (jobCoreContext.cancelPool[cancelPoolKey].IsCancellationRequested)
            {
                jobCoreContext.cancelPool.Remove(cancelPoolKey, out CancellationTokenSource cancellationTokenSource);
                jobCoreContext.taskPool.Remove(cancelPoolKey, out ManualResetEvent manualResetEvent);
                _logger.LogInformation("移除终止线程标志：" + cancelPoolKey);
            }
            
        }

        return ApiResult.succeed();
    }
    
    
}