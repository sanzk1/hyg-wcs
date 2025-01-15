using System.Transactions;

using dataPointsModule.Dal;
using dataPointsModule.Managers;
using domain.Dto;
using domain.Enums;
using domain.Pojo.protocol;
using domain.Records;
using infrastructure.Attributes;
using infrastructure.Exceptions;
using infrastructure.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MiniExcelLibs;

namespace dataPointsModule.Bll.Impl;

[Service]
public class OpcUaDataPointBll : IOpcUaDataPointBll
{
    private readonly IOpcUaDataPointDal _opcUaDataPointDal;
    private readonly ILogger<IOpcUaDataPointBll> _logger;
    private readonly IOpcUaManager _manager = ServiceUtil.GetRequiredService<IOpcUaManager>() ;

    public OpcUaDataPointBll(IOpcUaDataPointDal opcUaDataPointDal, ILogger<IOpcUaDataPointBll> logger)
    {
        _opcUaDataPointDal = opcUaDataPointDal;
        this._logger = logger;
    }

    public void Initializes()
    {
        List<string> selectEndpoints = _opcUaDataPointDal.SelectEndpoints();
        if (selectEndpoints.Count == 0)
            return;
        selectEndpoints.ForEach(item =>
        {
            _manager.Connect(new OpcUaDataPoint(){ endpoint = item});
        });
        
    }

    [TransactionScope(TransactionScopeOption.Required)]
    public void Save(OpcUaDataPoint point)
    {
        VerifyOpcUaDataPoint(point);
        if (point.id == 0)
        {
            OpcUaDataPoint? pointNew = _opcUaDataPointDal.SelectByNameAndOperate(point.name, point.operate);
            if (pointNew is not null)
            {
                throw new BusinessException("OpcUaDataPoint already exists");
            }
            _opcUaDataPointDal.Insert(point); 
        }
        else
        {
            OpcUaDataPoint? point1 = _opcUaDataPointDal.SelectByNameAndOperate(point.name, point.operate);
            OpcUaDataPoint? point2 = _opcUaDataPointDal.SelectById(point.id);
            if (point2 is null)
            {
                throw new BusinessException("OpcUaDataPoint is not exists");
                
            }
            if (point1 is not null && point1.id != point.id  )
            {
                throw new BusinessException("OpcUaDataPoint already exists");
            }
            _opcUaDataPointDal.Update(point);
        }

    }

    private void VerifyOpcUaDataPoint(OpcUaDataPoint point)
    {
        if (string.IsNullOrEmpty(point.name))
        {
            throw new BusinessException("Name is required");
        }
        if (!_manager.accessTypes.Any( a => point.accessType.Equals(a)))
        {
            throw new BusinessException("accessType is unknown");
        }
        if (string.IsNullOrEmpty(point.endpoint))
        {
            throw new BusinessException("endpoint is unknown");
        }   
        if (string.IsNullOrEmpty(point.identifier))
        {
            throw new BusinessException("identifier is unknown");
        }
        if (string.IsNullOrEmpty(point.dataType))
        {
            throw new BusinessException("dataType is unknown");
        }   
    }

    public DataPointDto ReadByName(string name)
    {
        var point = _opcUaDataPointDal.SelectByNameAndOperate(name, OperateEnum.Read);
        return _manager.Read(point);
    }

    public DataPointDto ReadById(long id)
    {
        var point = _opcUaDataPointDal.SelectById(id);
        if (point is null)
        {
            throw new BusinessException("数据点不存在");
        }
        return _manager.Read(point);
    }

    public DataPointDto WriteByName(string name, object value)
    {
        var point = _opcUaDataPointDal.SelectByNameAndOperate(name, OperateEnum.Write);
        if (point is null)
        {
            throw new BusinessException("数据点不存在");
        }
        return _manager.Write(point, value);
    }

    public DataPointDto WriteById(long id, object value)
    {
        var point = _opcUaDataPointDal.SelectById(id);
        if (point is null)
        {
            throw new BusinessException("数据点不存在");
        }
        return _manager.Write(point, value);
    }

    public OpcUaDataPoint getById(long id)
    {
        return _opcUaDataPointDal.SelectById(id);
    }

    [TransactionScope(TransactionScopeOption.Required)]
    public void DeleteBatch(List<long> ids)
    {
        if (ids.Count == 0)
        {
            return;
        }
        _opcUaDataPointDal.DeleteBatchById(ids);
    }

    public void ImportExcel(IFormFile file)
    {
        FileTypeEnum fileType = FileUtil.GetIFormFileType(Path.GetExtension(file.FileName));
        if (FileTypeEnum.Excel != fileType)
        {
            throw new BusinessException("文件类型异常，请选择excel文件");
        }

        List<OpcUaDataPoint> list = new();
        var rows = MiniExcel.Query<OpcUaPointDto>(file.OpenReadStream());
        foreach (var dto in rows.ToList())
        {
            OpcUaDataPoint point = new();
            point.endpoint = dto.endpoint;
            point.name = dto.name;
            point.dataType = dto.dataType;
            point.accessType = dto.accessType;
            point.operate = dto.operate;
            point.category = dto.category;
            point.remark = dto.remark;
            point.identifier = dto.identifier;
            
            list.Add(point);
        }
        _opcUaDataPointDal.InsertBacth(list);
    }

    public FileStreamResult ExportExcel(OpcUaDataPointQuery query)
    {
        try
        {
            List<OpcUaPointDto> list = _opcUaDataPointDal.SelectAll(query);
            var memoryStream = new MemoryStream();
            memoryStream.SaveAs(list);
            memoryStream.Seek(0, SeekOrigin.Begin);
            return new FileStreamResult(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                FileDownloadName = "OpcUa数据点.xlsx"
            };
        }
        catch (Exception ex)
        {
            _logger.LogError($"OpcUa数据点导出Excel失败, 原因：{ex.Message}");
            throw new BusinessException("sOpcUa数据点导出Excel失败");
        }

    }

    public Pager<OpcUaDataPoint> GetList(OpcUaDataPointQuery query)
    {
        return _opcUaDataPointDal.SelectList(query);
    }

    public void Delete(long id)
    {
        _opcUaDataPointDal.DeleteById(id);
    }
    
    
    
}