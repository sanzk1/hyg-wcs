
using domain.Dto;
using domain.Enums;
using domain.Pojo.jobCore;
using domain.Records;
using infrastructure.Utils;

namespace jobcoreModule.Bll;

public interface ITaskInfoBll
{

    void Save(TaskInfoDto dto);

    Pager<TaskInfo> GetList(TaskInfoQuery query);

    /// <summary>
    /// 根据任务状态获取任务列表
    /// </summary>
    /// <param name="executeStatus"></param>
    /// <returns></returns>
    List<TaskInfo> GetListByTaskState(TaskState executeStatus);

    void Del(List<string> ids);

    void Update(TaskInfo taskInfo);

    /// <summary>
    /// 控制任务状态
    /// </summary>
    /// <param name="taskId"></param>
    /// <param name="state"></param>
    void Scheduling(string taskId, TaskState state);

    /// <summary>
    /// 启动一个新的作业任务
    /// </summary>
    /// <param name="taskInfo"></param>
    void StartNewJob(TaskInfo taskInfo);

    /// <summary>
    /// 初始化将已启动的任务标记状态改为失败，并标记任务中断——项目启动时执行一次
    /// </summary>
    void Initialized();

}