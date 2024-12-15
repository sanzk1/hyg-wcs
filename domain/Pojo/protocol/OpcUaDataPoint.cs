using domain.Enums;
using SqlSugar;

namespace domain.Pojo.protocol;

/// <summary>
/// opcUa协议数据点
/// </summary>
public class OpcUaDataPoint
{
    [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
    public long id { set; get; }
    
    public string name { set; get; } = string.Empty;
    /// <summary>
    /// 类别
    /// </summary>
    public string category { set; get; } = string.Empty;
    
    public string endpoint { set; get; } = string.Empty;
    
    public int namespaceIndex { set; get; } = 2;
    
    public string identifier { set; get; } = string.Empty;
    /// <summary>
    /// 访问类型 索引i  字符串s Guid g
    /// </summary>
    public string accessType { set; get; } = "s";
    /// <summary>
    /// 数据类型
    /// </summary>
    public string dataType { set; get; } = string.Empty;
    /// <summary>
    /// 备注
    /// </summary>
    public string remark { set; get; } = string.Empty;

    /// <summary>
    /// 其他
    /// </summary>
    public int hardwareType { set; get; } = 0;
    /// <summary>
    /// 读写操作
    /// </summary>
    public OperateEnum operate { set; get; } = OperateEnum.Read;
    
}