using adminModule.Bll;
using domain.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace adminModule.Controllers;


[ApiController]
[Route("[controller]/[action]")]
public class SysFileInfoController : ControllerBase
{

    private readonly ILogger<SysFileInfoController> _logger;
    private readonly ISysFileInfoBll _sysFileInfoBll;

    public SysFileInfoController(ILogger<SysFileInfoController> logger, ISysFileInfoBll sysFileInfoBll)
    {
        _logger = logger;
        _sysFileInfoBll = sysFileInfoBll;
    }
    

    [HttpPost]
    public ApiResult UploadFile(IFormFile file)
    {
        return ApiResult.succeed(_sysFileInfoBll.SaveFileInfo(file));
    }
    
    
}