using api.Common.DTO;
using dataPointsModule.Dal;
using dataPointsModule.Managers;
using domain.Pojo.protocol;
using domain.Records;
using domain.Result;
using infrastructure.Attributes;
using infrastructure.Exceptions;
using infrastructure.Utils;
using Microsoft.Extensions.Logging;
using Yitter.IdGenerator;

namespace dataPointsModule.Bll.Impl;

[Service]
public class ModbusDataBll : IModbusDataBll
{
    private readonly ILogger<ModbusDataBll> _logger;
    private readonly IModbusDataDal _modbusDataDal;
    private readonly IModbusManager _manager;

    public ModbusDataBll(ILogger<ModbusDataBll> logger, IModbusDataDal modbusDataDal, IModbusManager manager)
    {
        this._logger = logger;
        this._modbusDataDal = modbusDataDal;
        this._manager = manager;
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
    
}