using dataPointsModule.Bll;
using domain.Records;
using domain.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiniExcelLibs;

namespace dataPointsModule.Controller;

[Authorize]
[ApiController]
[Route("[controller]/[action]")]
public class ProtocolLogController : ControllerBase
{
    private IProtocolLogBll _protocolLogBll;
    private ILogger<ProtocolLogController> _logger;
    public ProtocolLogController(IProtocolLogBll protocolLogBll, ILogger<ProtocolLogController> logger)
    {
        this._logger = logger;
        this._protocolLogBll = protocolLogBll;
    }

    [HttpPost]
    public ApiResult getList([FromBody] ProtocolLogQuery protocolLogQuery)
    {
        return ApiResult.succeed(_protocolLogBll.GetList(protocolLogQuery));
    }

    [HttpDelete]
    public ApiResult del(List<long> ids)
    {
        _protocolLogBll.Delete(ids);
        return ApiResult.succeed();
    }

    [HttpPost]
    public IActionResult exportExcel([FromBody] ProtocolLogQuery protocolLogQuery  ){
        var memoryStream = new MemoryStream();
        memoryStream.SaveAs(_protocolLogBll.GetList(protocolLogQuery).rows);
        memoryStream.Seek(0, SeekOrigin.Begin);

        return new FileStreamResult(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                FileDownloadName = $"数据读写记录-{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx"
            };
    }

    
}