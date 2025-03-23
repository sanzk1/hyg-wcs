using domain.Enums;
using MiniExcelLibs.Attributes;

namespace domain.Dto;

public class S7PointDto
{
    /// <summary>
    /// 数据点id
    /// </summary>
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
    /// 芯片型号
    /// </summary>
    [ExcelColumn(Name = "芯片型号", Width = 10)]
    public string cpuType { set; get; } = "S71200";
    /// <summary>
    /// 端口号
    /// </summary>
    [ExcelColumn(Name = "端口号", Width = 10)]
    public int port { set; get; } = 102;
    
    [ExcelColumn(Name = "机架号", Width = 10)]
    public int rack { set; get; } = 0;
    
    [ExcelColumn(Name = "插槽", Width = 10)]
    public int slot { set; get; } = 1;
    /// <summary>
    /// db块
    /// </summary>
    [ExcelColumn(Name = "db块", Width = 10)]
    public int db { set; get; } = 1;
    /// <summary>
    /// 开始地址 || 偏移量
    /// </summary>
    [ExcelColumn(Name = "偏移量", Width = 10)]
    public int startAddress { set; get; } = 0;
    /// <summary>
    /// 长度
    /// </summary>
    [ExcelColumn(Name = "长度", Width = 10)]
    public int length { set; get; } = 1;
    /// <summary>
    /// 数据类型
    /// </summary>
    [ExcelColumn(Name = "数据类型", Width = 10)]
    public string dataType { set; get; } = string.Empty;
    /// <summary>
    /// 备注
    /// </summary>
    [ExcelColumn(Name = "备注", Width = 30)]
    public string remark { set; get; } = string.Empty;
    /// <summary>
    /// 读写操作
    /// </summary>
    [ExcelColumn(Name = "读写", Width = 10)]
    public OperateEnum operate { set; get; } = OperateEnum.Read;
}