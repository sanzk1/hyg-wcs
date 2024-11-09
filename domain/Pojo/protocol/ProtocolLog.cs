using SqlSugar;

namespace domain.Pojo.protocol;

public class ProtocolLog
{
    [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
    public long id { set; get; }
    /// <summary>
    /// 数据点id
    /// </summary>
    public string name { set; get; } = string.Empty;
    /// <summary>
    /// 类别
    /// </summary>
    public string category { set; get; } = string.Empty;

    /// <summary>
    /// 状态
    /// </summary>
    public bool status { set; get; } = true;
    /// <summary>
    /// 消耗时间
    /// </summary>
    public long time { set; get; } = 0;
    /// <summary>
    /// 异常原因
    /// </summary>
    public string reson { set; get; } = string.Empty;
    /// <summary>
    /// 操作
    /// </summary>
    public string oper { set; get; } = string.Empty;
    /// <summary>
    /// 数据点值
    /// </summary>
    public string value { set; get; } = string.Empty;
    /// <summary>
    /// 读取时间
    /// </summary>
    public DateTime createdTime { set; get; } = DateTime.Now;
    /// <summary>
    /// 读取结束时间
    /// </summary>
    public DateTime endTime { set; get; } = DateTime.Now;
    
    
    
    
}