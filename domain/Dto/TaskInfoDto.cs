namespace domain.Dto;

public class TaskInfoDto
{

        /// <summary>
        /// 任务名称
        /// </summary>
        public string taskName { set; get; } = string.Empty;
        
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

}