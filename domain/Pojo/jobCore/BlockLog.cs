using SqlSugar;

namespace domain.Pojo.jobCore;

public class BlockLog
{
    [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
    public int id { set; get; } = 0;
    /// <summary>
    /// 任务id
    /// </summary>
    public string taskId { set; get; } = string.Empty;
    /// <summary>
    /// 动作快id
    /// </summary>
    public string bId { set; get; } = string.Empty;
    /// <summary>
    /// 值
    /// </summary>
    public string value { set; get; } = string.Empty;
    /// <summary>
    /// 描述
    /// </summary>
    public string txt { set; get; } = string.Empty;
    /// <summary>
    /// 动作快是否完成
    /// </summary>
    public bool executeStatus { set; get; } = false;

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime createTime { set; get; } = DateTime.Now;
    /// <summary>
    /// 任务完成时间
    /// </summary>
    public DateTime? updateTIme { set; get; } 

}