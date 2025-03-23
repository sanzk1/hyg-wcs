
using domain.Dto;
using domain.Pojo.protocol;
using domain.Records;
using infrastructure.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dataPointsModule.Bll;

public interface IS7DataPointBll
{
    /// <summary>
    /// 设备连接初始化
    /// </summary>
    public void Initializes();

    public Pager<S7DataPoint> GetList(S7DataPointQuery query);
    
    public void Save(S7DataPoint point);

    public void Del(List<long> ids);

    public void Update(S7DataPoint point);

    public S7DataPoint GetById(long id);

    public List<S7DataPoint> GetAll();

    /// <summary>
    /// 批量保存数据点
    /// </summary>
    /// <param name="points"></param>
    public void BatchSave(List<S7DataPoint> points);

    /// <summary>
    /// 根据数据点名称读单个数据
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public DataPointDto ReadByName(string name);
    /// <summary>
    /// 根据数据点名称写单个数据
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public DataPointDto WriteByName(string name, object value);
    
    public FileStreamResult ExportExcel(S7DataPointQuery query);

    public void importExcel(IFormFile file);

}