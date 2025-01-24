using MiniExcelLibs.Attributes;

namespace domain.Dto;

public class ModbusPointDto
{
    [ExcelColumn(Name = "数据点id", Width = 20)]
    public string name { set; get; } = string.Empty;
    /// <summary>
    /// 类别
    /// </summary>
    [ExcelColumn(Name = "类别", Width = 20)]
    public string category { set; get; } = string.Empty;
    /// <summary>
    /// ip
    /// </summary>
    [ExcelColumn(Name = "ip", Width = 20)]
    public string ip { set; get; } = string.Empty;
    /// <summary>
    /// 端口号
    /// </summary>
    [ExcelColumn(Name = "端口", Width = 10)]
    public int port { set; get; } = 502;
    /// <summary>
    /// 从站
    /// </summary>
    [ExcelColumn(Name = "从站地址", Width = 10)]
    public int stationNo { set; get; } = 1;
    /// <summary>
    /// 开始地址
    /// </summary>
    [ExcelColumn(Name = "开始地址", Width = 10)]
    public int startAddress { set; get; } = 0;
    /// <summary>
    /// 数据类型
    /// </summary>
    [ExcelColumn(Name = "数据类型", Width = 10)]
    public string dataType { set; get; } = "ushort";
    /// <summary>
    /// 字节格式
    /// </summary>
    [ExcelColumn(Name = "字节格式（ABCD|BADC|CDAB|DCBA）", Width = 10)]
    public string format { set; get; } = "ABCD";
    /// <summary>
    /// 长度
    /// </summary>
    [ExcelColumn(Name = "长度", Width = 10)]
    public int length { set; get; } = 1;
    /// <summary>
    /// 是否只读
    /// </summary>
    [ExcelColumn(Name = "是否只读（DI）", Width = 10)]
    public bool readOnly { set; get; } = false;
    /// <summary>
    /// 备注
    /// </summary>
    [ExcelColumn(Name = "备注", Width = 10)]
    public string remark { set; get; } = string.Empty;
}