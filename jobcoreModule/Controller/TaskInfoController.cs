using api.Common.DTO;
using domain.Pojo.jobCore;
using domain.Result;
using jobcoreModule.Bll;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace jobcoreModule.Controller;


[Route("[controller]/[action]")]
[ApiController]
public class TaskInfoController : ControllerBase
{
    private readonly ITaskInfoBll taskInfoBll;
    private readonly ILogger<TaskInfoController> _logger;

    public TaskInfoController(ILogger<TaskInfoController> logger, ITaskInfoBll taskInfoBll)
    {
        this._logger = logger;
        this.taskInfoBll = taskInfoBll;
    }


    [HttpPost]
    public ApiResult createdTask([FromBody] TaskInfoDto taskInfo)
    {
        taskInfoBll.Save(taskInfo);
        return ApiResult.succeed();
    }
    
}