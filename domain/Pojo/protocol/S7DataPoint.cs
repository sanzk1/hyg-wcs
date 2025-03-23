using domain.Enums;
using SqlSugar;

namespace domain.Pojo.protocol;

/// <summary>
/// S7协议数据点
/// </summary>
public class S7DataPoint
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
    /// ip
    /// </summary>
    public string ip { set; get; } = string.Empty;
    /// <summary>
    /// 芯片型号
    /// </summary>
    public string cpuType { set; get; } = "S71200";
    /// <summary>
    /// 端口号
    /// </summary>
    public int port { set; get; } = 102;
    
    public int rack { set; get; } = 0;
    
    public int slot { set; get; } = 1;
    /// <summary>
    /// db块
    /// </summary>
    public int db { set; get; } = 1;
    /// <summary>
    /// 开始地址 || 偏移量
    /// </summary>
    public int startAddress { set; get; } = 0;
    /// <summary>
    /// 长度
    /// </summary>
    public int length { set; get; } = 1;
    /// <summary>
    /// 数据类型
    /// </summary>
    public string dataType { set; get; } = string.Empty;
    /// <summary>
    /// 位bit
    /// </summary>
    public int bit { set; get; } = 0;
    /// <summary>
    /// 值
    /// </summary>
    public int value { set; get; } = 0;
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