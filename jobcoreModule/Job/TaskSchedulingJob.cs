using domain.Enums;
using domain.Pojo.quartz;
using infrastructure.Db;
using infrastructure.Utils;
using jobcoreModule.Bll;
using jobcoreModule.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;
using quartzModeule.Bll;

namespace jobcoreModule.Job;

public class TaskSchedulingJob : IJob
{
   // private readonly DbClientFactory _dbClientFactory = ServiceUtil.ServiceProvider.GetService<DbClientFactory>();
    private readonly JobCoreContext jobCoreContext = ServiceUtil.ServiceProvider.GetService<JobCoreContext>();
    private readonly ITaskInfoBll taskInfoBll = ServiceUtil.ServiceProvider.GetService<ITaskInfoBll>();
    private readonly IJobLogBll _jobLogBll = ServiceUtil.ServiceProvider.GetService<IJobLogBll>();
    private readonly ILogger<TaskSchedulingJob> logger = ServiceUtil.ServiceProvider.GetService<ILogger<TaskSchedulingJob>>();


    public async Task Execute(IJobExecutionContext context)
    {
        JobLog jobLog = new JobLog();
        jobLog.jobName = "任务调度核心";
        jobLog.category = "作业调度";
        jobLog.typeName = "quartzModeule.Job.TestJob";
        try
        {
            logger.LogInformation("测试定时任务------" + DateTime.Now.ToString("HH:mm:ss zz"));
            Thread.Sleep(1000);
            
            // 查询已创建的任务，下发执行
            taskInfoBll.GetListByTaskState(TaskState.Created).ForEach(item =>
            {
                taskInfoBll.StartNewJob(item);
            });
            
            // 释放已终止和已完成的任务线程信号资源
            taskInfoBll.GetListByTaskState(TaskState.Complete).ForEach(item =>
            {
                jobCoreContext.RemoveThread(item.id);
            });
            taskInfoBll.GetListByTaskState(TaskState.Terminate).ForEach(item =>
            {
                jobCoreContext.RemoveThread(item.id);
            });
            
            jobLog.status = true;
            jobLog.stopTime = DateTime.Now;
        }
        catch (Exception e)
        {
            jobLog.exceptionInfo = e.Message;
        }
        _jobLogBll.Save(jobLog);
    }
}