using domain.Enums;
using MiniExcelLibs.Attributes;

namespace domain.Dto;

public class OpcUaPointDto
{
    [ExcelColumn(Name = "数据点id", Width = 20)]
    public string name { set; get; } = string.Empty;
    /// <summary>
    /// 类别
    /// </summary>
    [ExcelColumn(Name = "类别", Width = 20)]
    public string category { set; get; } = string.Empty;
    
    [ExcelColumn(Name = "endpoint", Width = 20)]
    public string endpoint { set; get; } = string.Empty;
    
    [ExcelColumn(Name = "namespaceIndex", Width = 15)]
    public int namespaceIndex { set; get; } = 2;
    
    [ExcelColumn(Name = "identifier", Width = 10)]
    public string identifier { set; get; } = string.Empty;
    /// <summary>
    /// 访问类型 索引i  字符串s Guid g
    /// </summary>
    [ExcelColumn(Name = "accessType", Width = 10)]
    public string accessType { set; get; } = "s";
    /// <summary>
    /// 数据类型
    /// </summary>
    [ExcelColumn(Name = "dataType", Width = 10)]
    public string dataType { set; get; } = string.Empty;
    /// <summary>
    /// 备注
    /// </summary>
    [ExcelColumn(Name = "备注", Width = 20)]
    public string remark { set; get; } = string.Empty;

    /// <summary>
    /// 读写操作
    /// </summary>
    [ExcelColumn(Name = "读写操作", Width = 10)]
    public OperateEnum operate { set; get; } = OperateEnum.Read;
}