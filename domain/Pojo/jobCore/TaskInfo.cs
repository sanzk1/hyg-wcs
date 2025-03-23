using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using domain.Enums;
using SqlSugar;

namespace domain.Pojo.jobCore
{
    public class TaskInfo
    {
        /// <summary>
        /// 任务id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public string id { set; get; } = string.Empty;

        /// <summary>
        /// 任务名称
        /// </summary>
        public string taskName { set; get; } = string.Empty;

        /// <summary>
        /// 任务状态
        /// </summary>
        public TaskState executeStatus { set; get; } = TaskState.Created;
        /// <summary>
        /// 是否中断 
        /// </summary>
        public int interrupt { set; get; } = 0;
        /// <summary>
        /// 任务异常原因
        /// </summary>
        public string reason { set; get; } = string.Empty;

        /// <summary>
        /// 输入参数
        /// </summary>
        public string inputParam { set; get; } = string.Empty;
        /// <summary>
        /// 输出参数
        /// </summary>
        public string outputParam { set; get; } = string.Empty;

        /// <summary>
        /// 任务类型
        /// </summary>
        public int taskType { set; get; } = -1;
        
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime createTime { set; get; } = DateTime.Now;
        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime finishTime { set; get; } = DateTime.Now;
        
        /// <summary>
        /// 创建人
        /// </summary>
        public string createdBy { set; get; } = string.Empty;
        /// <summary>
        /// 更新人
        /// </summary>
        public string updatedBy { set; get; } = string.Empty;

    }
}
