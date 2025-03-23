using dataPointsModule.Bll;
using domain.Pojo.protocol;
using domain.Records;
using domain.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace dataPointsModule.Controller;


[Authorize]
[ApiController]
[Route("[controller]/[action]")]
public class OpcUaDataPointController  : ControllerBase
{
    
    private readonly ILogger<OpcUaDataPointController> _logger;
    private readonly IOpcUaDataPointBll _opcUaDataPointBll;

    public OpcUaDataPointController(ILogger<OpcUaDataPointController> logger, IOpcUaDataPointBll opcUaDataPointBll)
    {
        this._logger = logger;
        this._opcUaDataPointBll = opcUaDataPointBll;
    }

    [HttpPost]
    public ApiResult getList([FromBody] OpcUaDataPointQuery query)
    {
        return ApiResult.succeed(_opcUaDataPointBll.GetList(query));
    }

    [HttpPost]
    public ApiResult addOrUpdate([FromBody] OpcUaDataPoint point)
    {
        _opcUaDataPointBll.Save(point);
        return ApiResult.succeed();
    }

    [HttpDelete]
    public ApiResult del([FromBody] List<long> ids)
    {
        _opcUaDataPointBll.DeleteBatch(ids);
        return ApiResult.succeed();
    }

    [HttpGet("{id}")]
    public ApiResult readById(long id)
    {
        return ApiResult.succeed(_opcUaDataPointBll.ReadById(id));
    }
    [HttpGet("{id}")]
    public ApiResult get(long id)
    {
        return ApiResult.succeed(_opcUaDataPointBll.getById(id));
    }

    [HttpPost]
    public ApiResult writeById(long id, string value)
    {
        return ApiResult.succeed(_opcUaDataPointBll.WriteById(id, value));
    }

    [HttpPost]
    public ApiResult importExcel(IFormFile file)
    {
        _opcUaDataPointBll.ImportExcel(file);
        return ApiResult.succeed();
    }
    [HttpPost]
    public IActionResult exportExcel([FromBody] OpcUaDataPointQuery query)
    {
        return  _opcUaDataPointBll.ExportExcel(query);
    }
    
    
}