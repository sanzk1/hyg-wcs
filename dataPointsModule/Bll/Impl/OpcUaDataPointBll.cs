using System.Transactions;
using api.Common.DTO;
using dataPointsModule.Dal;
using dataPointsModule.Managers;
using domain.Enums;
using domain.Pojo.protocol;
using domain.Records;
using infrastructure.Attributes;
using infrastructure.Exceptions;
using infrastructure.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace dataPointsModule.Bll.Impl;

[Service]
public class OpcUaDataPointBll : IOpcUaDataPointBll
{
    private readonly IOpcUaDataPointDal _opcUaDataPointDal;
    private readonly IOpcUaManager _manager;

    public OpcUaDataPointBll(IOpcUaDataPointDal opcUaDataPointDal, IOpcUaManager manager)
    {
        _opcUaDataPointDal = opcUaDataPointDal;
        _manager = manager;
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
        if (point.id != 0)
        {
            OpcUaDataPoint? pointNew = _opcUaDataPointDal.SelectByNameAndOperate(point.name, point.operate);
            if (pointNew != null)
            {
                throw new BusinessException("OpcUaDataPoint already exists");
            }
            _opcUaDataPointDal.Insert(point); 
        }
        else
        {
            OpcUaDataPoint? point1 = _opcUaDataPointDal.SelectByNameAndOperate(point.name, point.operate);
            if (point1 != null && point1.id != point.id)
            {
                throw new BusinessException("OpcUaDataPoint already exists");
            }
            OpcUaDataPoint? point2 = _opcUaDataPointDal.SelectById(point.id);
            if (point2 != null)
            {
                throw new BusinessException("OpcUaDataPoint is not exists");
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

    [TransactionScope(TransactionScopeOption.Required)]
    public void DeleteBatch(List<long> ids)
    {
        if (ids.Count == 0)
        {
            return;
        }
        _opcUaDataPointDal.DeleteBatchById(ids);
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