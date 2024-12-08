using domain.Enums;
using SqlSugar;

namespace domain.Pojo.sys;

/// <summary>
/// 文件上传信息记录
/// </summary>
public class SysFileInfo
{
    [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
    public long id { get; set; }
    public string fileName { get; set; } = string.Empty;
    public string url { get; set; } = string.Empty;
    /// <summary>
    /// 存储路径
    /// </summary>
    public string path { get; set; } = string.Empty;
    /// <summary>
    /// 文件后缀
    /// </summary>
    public string suffix { get; set; } = string.Empty;
    /// <summary>
    /// 文件类型
    /// </summary>
    public FileTypeEnum fileType { get; set; } = FileTypeEnum.Other;
    /// <summary>
    /// 是否删除
    /// </summary>
    public bool isDelete { get; set; } = false;
    
    public DateTime createdTime { get; set; } = DateTime.Now;
    /// <summary>
    /// 文件大小
    /// </summary>
    public long size { get; set; } = 0;
}