using domain.Enums;
using SqlSugar;

namespace domain.Pojo.protocol;


/// <summary>
/// Modbus数据点
/// </summary>
public class ModbusDataPoint
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
    /// 端口号
    /// </summary>
    public int port { set; get; } = 502;
    /// <summary>
    /// 从站
    /// </summary>
    public int stationNo { set; get; } = 1;
    /// <summary>
    /// 开始地址
    /// </summary>
    public int startAddress { set; get; } = 0;
    /// <summary>
    /// 数据类型
    /// </summary>
    public string dataType { set; get; } = "ushort";
    /// <summary>
    /// 字节格式
    /// </summary>
    public string format { set; get; } = "ABCD";
    /// <summary>
    /// 长度
    /// </summary>
    public int length { set; get; } = 1;
    /// <summary>
    /// 是否只读
    /// </summary>
    public bool readOnly { set; get; } = false;
    /// <summary>
    /// 值
    /// </summary>
    public object value { set; get; } = 0;
    /// <summary>
    /// 备注
    /// </summary>
    public string remark { set; get; } = string.Empty;
    
    /// <summary>
    /// 其他
    /// </summary>
    public int hardwareType { set; get; } = 0;

}