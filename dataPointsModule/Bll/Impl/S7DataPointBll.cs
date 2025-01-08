using System.Text;
using api.Common.DTO;
using dataPointsModule.Attributes;
using dataPointsModule.Managers;
using domain.Enums;
using domain.Pojo.protocol;
using domain.Records;
using domain.Result;
using infrastructure.Attributes;
using infrastructure.Db;
using infrastructure.Exceptions;
using infrastructure.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using S7.Net;
using SqlSugar;

namespace dataPointsModule.Bll.Impl;

[Service]
public class S7DataPointBll : IS7DataPointBll
{
    private readonly ILogger<S7DataPointBll> _logger;
    private readonly DbClientFactory _dbClientFactory;
    private readonly IS7Manager _manager;

    public S7DataPointBll(ILogger<S7DataPointBll> logger, DbClientFactory dbClientFactory, IS7Manager manager)
    {
        this._logger = logger;
        this._dbClientFactory = dbClientFactory;
        this._manager = manager;
    }

    private record Device(string ip, int port);

    public void Initializes()
    {
        using var db = _dbClientFactory.GetSqlSugarClient();
        List<Device> sDevices = db.Queryable<S7DataPoint>().Select<Device>(x => new Device(x.ip, x.port)).Distinct().ToList();
        List<S7DataPoint> list = new();
        sDevices.ForEach(item =>
        {
            S7DataPoint? s7 = db.Queryable<S7DataPoint>().First(x => x.ip.Equals(item.ip) && x.port == item.port);
            if (s7 != null)
            {
                list.Add(s7);
            }
        });
        if (list.Count == 0)
        {
            return;
        }
        list.ForEach(item =>
        {
            _manager.Connect(item);
        });
    }

   
    public Pager<S7DataPoint> GetList(S7DataPointQuery query)
    {
        Pager<S7DataPoint> pager = new(query.pageNum, query.pageSize);
        var exp = Expressionable.Create<S7DataPoint>();
        exp.AndIF(!string.IsNullOrEmpty(query.name), x => x.name.Contains(query.name));
        exp.AndIF(!string.IsNullOrEmpty(query.name), x => x.category.Contains(query.category));
        exp.AndIF( query.startAddress != null, x => x.startAddress == query.startAddress);
        using var db = _dbClientFactory.GetSqlSugarClient();
        pager.rows = db.Queryable<S7DataPoint>().Where(exp.ToExpression()).Skip(pager.getSkip())
            .Take(pager.pageSize).ToList();
        pager.total = db.Queryable<S7DataPoint>().Where(exp.ToExpression()).Count();
        return pager;
    }


    public void Save(S7DataPoint point)
    {
        using var db = _dbClientFactory.GetSqlSugarClient();
        S7DataPoint? s7DataPoint = db.Queryable<S7DataPoint>().Where(x => x.name.Equals(point.name)).First();
        if (s7DataPoint != null)
        {
            throw new BusinessException("数据点名称已存在");
        }
        db.Insertable(point).ExecuteCommand();
    }

    public void Del(List<long> ids)
    {
        if (ids.Count == 0)
        {
            return;
        }
        using var db = _dbClientFactory.GetSqlSugarClient();
        db.Deleteable<S7DataPoint>().In(ids).ExecuteCommand();
    }

    public void Update(S7DataPoint point)
    {
        using var db = _dbClientFactory.GetSqlSugarClient();
        S7DataPoint s7point = db.Queryable<S7DataPoint>().First(x => x.id == point.id);
        if (s7point is null)
        {
            throw new BusinessException(HttpCode.FAILED_CODE, "数据点不存在");
        }
        db.Updateable(point);
    }

    public S7DataPoint GetById(long id)
    {
        using var db = _dbClientFactory.GetSqlSugarClient();
        return db.Queryable<S7DataPoint>().First(x => x.id == id);
    }

    public List<S7DataPoint> GetAll()
    {
        using var db = _dbClientFactory.GetSqlSugarClient();
        return db.Queryable<S7DataPoint>().ToList();
    }

    public void BatchSave(List<S7DataPoint> points)
    {
        using var db = _dbClientFactory.GetSqlSugarClient();
        db.Ado.BeginTran();
        try
        {
            db.Insertable<S7DataPoint>(points).ExecuteCommand();
            db.Ado.CommitTran();
        }
        catch (Exception ex)
        {
            db.Ado.RollbackTran();
            throw new BusinessException(500, ex.Message);
        }
    }

    public DataPointDto ReadByName(string name)
    {
        using var db = _dbClientFactory.GetSqlSugarClient();
        S7DataPoint? point = db.Queryable<S7DataPoint>()
            .Where(x => name.Equals(x.name) && x.operate == OperateEnum.Read).First();
        if (point is null)
        {
            throw new BusinessException(HttpCode.FAILED_CODE, "数据点不存在");
        }
        return _manager.Read(point);
    }

    public DataPointDto WriteByName(string name, object value)
    {
        using var db = _dbClientFactory.GetSqlSugarClient();
        S7DataPoint? point = db.Queryable<S7DataPoint>()
            .Where(x => name.Equals(x.name) && x.operate == OperateEnum.Write).First();
        if (point is null)
        {
            throw new BusinessException(HttpCode.FAILED_CODE, "数据点不存在");
        }
        return _manager.Write(point, value);
    }
    
    
    
}