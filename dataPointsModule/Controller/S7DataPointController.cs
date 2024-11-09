using dataPointsModule.Bll;
using dataPointsModule.Managers;
using domain.Pojo.protocol;
using domain.Records;
using domain.Result;
using infrastructure.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniExcelLibs;
using S7.Net;

namespace dataPointsModule.Controller;

[Authorize]
[Route("[controller]/[action]")]
[ApiController]
public class S7DataPointController: ControllerBase
{
    private IS7DataPointBll _s7DataPointBll;
    private IS7Manager _manager;
    public S7DataPointController(IS7DataPointBll s7DataPointBll, IS7Manager manager)
    {
        _s7DataPointBll = s7DataPointBll;
        this._manager = manager;
    }

    [HttpGet("{id}")]
    public ApiResult getId([FromRoute] long id)
    {
        return ApiResult.succeed(_s7DataPointBll.GetById(id));
    }
    
    [HttpDelete]
    public ApiResult del([FromBody]List<long> ids)
    {
        _s7DataPointBll.Del(ids);
        return ApiResult.succeed();
    }

    [HttpGet]
    public ApiResult exportExcel()
    {
        var path = Path.Combine("D:\\work", $"{Guid.NewGuid()}.xlsx");
        MiniExcel.SaveAs(path, _s7DataPointBll.GetAll());
        return ApiResult.succeed();
    }

    [HttpPost]
    public ApiResult importExcel( IFormFile file)
    {
        var rows = MiniExcel.Query<S7DataPoint>(file.OpenReadStream());
        _s7DataPointBll.BatchSave(rows.ToList());
        return ApiResult.succeed();
    }

    [HttpPut]
    public ApiResult update([FromBody] S7DataPoint point)
    {
        checkPoint(point);
        _s7DataPointBll.Update(point);
        return ApiResult.succeed();
    }
    
    [HttpPost]
    public ApiResult getList([FromBody]S7DataPointQuery query)
    {
        return ApiResult.succeed(_s7DataPointBll.GetList(query));
    }

    [HttpGet("{id}")]
    public ApiResult readS7([FromRoute] long id)
    {
        S7DataPoint point = _s7DataPointBll.GetById(id);
        if (point == null)
        {
            return ApiResult.failed(400, "数据点不存在");
        }
        return ApiResult.succeed(_manager.Read(point));
    }
    [HttpPost]
    public ApiResult writeS7(long id, string value)
    {
        S7DataPoint point = _s7DataPointBll.GetById(id);
        if (point == null)
        {
            return ApiResult.failed(400, "数据点不存在");
        }
        return ApiResult.succeed(_manager.Write(point, value));
    }  
    [HttpPost]
    public ApiResult read7Point(string name)
    {
        return ApiResult.succeed(_s7DataPointBll.ReadByName(name));
    }
    [HttpPost]
    public ApiResult writeS7Point(string name, string value)
    {
        _s7DataPointBll.WriteByName(name, value);
        return ApiResult.succeed();
    }

    [HttpPost]
    public ApiResult add(S7DataPoint point)
    {
        checkPoint(point);
        _s7DataPointBll.Save(point);
        return ApiResult.succeed();
    }

    private void checkPoint(S7DataPoint point)
    {
        if (string.IsNullOrEmpty(point.name))
        {
            throw new BusinessException("数据点名称不能为空");
        }
        if (string.IsNullOrEmpty(point.ip))
        {
            throw new BusinessException("数据点IP不能为空");
        }
    }

    [HttpGet]
    public ApiResult getCpuType()
    {
        return ApiResult.succeed(Enum.GetNames(typeof(CpuType)));
    }
    [HttpGet]
    public ApiResult getVarType()
    {
        return ApiResult.succeed(Enum.GetNames(typeof(VarType)));
    }
    
    
}