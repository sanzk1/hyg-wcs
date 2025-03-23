
using dataPointsModule.Dal;
using dataPointsModule.Managers;
using domain.Dto;
using domain.Enums;
using domain.Pojo.protocol;
using domain.Records;
using domain.Result;
using infrastructure.Attributes;
using infrastructure.Exceptions;
using infrastructure.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiniExcelLibs;
using Yitter.IdGenerator;

namespace dataPointsModule.Bll.Impl;

[Service]
public class ModbusDataBll : IModbusDataBll
{
    private readonly ILogger<ModbusDataBll> _logger;
    private readonly IModbusDataDal _modbusDataDal;
    private readonly IModbusManager _manager = ServiceUtil.GetRequiredService<IModbusManager>() ;

    public ModbusDataBll(ILogger<ModbusDataBll> logger, IModbusDataDal modbusDataDal)
    {
        this._logger = logger;
        this._modbusDataDal = modbusDataDal;
    }
    
    public void Save(ModbusDataPoint modbusDataPoint)
    {
        ModbusDataPoint m1 = _modbusDataDal.SelectByName(modbusDataPoint.name);
        if (null != m1)
        {
            throw new BusinessException(HttpCode.FAILED_CODE, "数据点名称已存在");
        }
        modbusDataPoint.id = YitIdHelper.NextId();
        _modbusDataDal.Insert(modbusDataPoint);
    }

    public void Remove(List<long> ids)
    {
        if (ids.Count == 0)
        {
            return;
        }
        _modbusDataDal.Delete(ids);
    }

    public Pager<ModbusDataPoint> GetList(ModbusDataQuery query)
    {
        return _modbusDataDal.pager(query);
    }

    public ModbusDataPoint Get(long id)
    {
        return _modbusDataDal.SelectById(id);
    }


    public DataPointDto ReadById(long id)
    {
        ModbusDataPoint m = _modbusDataDal.SelectById(id);
        if (m is null)
        {
            throw new BusinessException(HttpCode.FAILED_CODE, "数据点不存在");
        }
        return _manager.Read(m);
    }

    public DataPointDto ReadByName(string name)
    {
        ModbusDataPoint m = _modbusDataDal.SelectByName(name);
        if (m is null)
        {
            throw new BusinessException(HttpCode.FAILED_CODE, "数据点不存在");
        }
        return _manager.Read(m);
    }

    public DataPointDto WriteByName(string name, object value)
    {
        ModbusDataPoint m = _modbusDataDal.SelectByName(name);
        if (m is null)
        {
            throw new BusinessException(HttpCode.FAILED_CODE, "数据点不存在");
        }
        return _manager.Write(m, value);
    }

    public DataPointDto WriteById(long id, object value)
    {
        ModbusDataPoint m = _modbusDataDal.SelectById(id);
        if (m is null)
        {
            throw new BusinessException(HttpCode.FAILED_CODE, "数据点不存在");
        }
        return _manager.Write(m, value);
    }

    public FileResult ExportExcel(ModbusDataQuery query)
    {
        var memoryStream = new MemoryStream();
        memoryStream.SaveAs(_modbusDataDal.SelectList(query));
        memoryStream.Seek(0, SeekOrigin.Begin);
        return new FileStreamResult(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = "Modbus数据点.xlsx"
        };
    }

    public void ImportExcel(IFormFile file)
    {
        FileTypeEnum fileType = FileUtil.GetIFormFileType(Path.GetExtension(file.FileName));
        if (FileTypeEnum.Excel != fileType)
        {
            throw new BusinessException("文件类型异常，请选择excel文件");
        }

        List<ModbusDataPoint> list = new();
        var rows = MiniExcel.Query<ModbusPointDto>(file.OpenReadStream());
        
        list.ForEach(item =>
        {
            ModbusDataPoint modbusDataPoint = new();
            modbusDataPoint.name = item.name;
            modbusDataPoint.category = item.category;
            modbusDataPoint.ip = item.ip;
            modbusDataPoint.port = item.port;
            modbusDataPoint.dataType = item.dataType;
            modbusDataPoint.startAddress = item.startAddress;
            modbusDataPoint.stationNo = item.stationNo;
            modbusDataPoint.length = item.length;
            modbusDataPoint.readOnly = item.readOnly;
            modbusDataPoint.format = item.format;
            modbusDataPoint.remark = item.remark;
            list.Add(item);
        });
        _modbusDataDal.BatchInsert(list);
    }
}