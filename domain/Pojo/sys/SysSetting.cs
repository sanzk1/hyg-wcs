using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace domain.Pojo.sys
{
    /// <summary>
    /// 系统配置
    /// </summary>
    public class SysSetting
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public long id {set;get;}
        /// <summary>
        /// 键名称
        /// </summary>
        public string keyName{set;get;} = string.Empty;

        public string value{set;get;} = string.Empty;

    }
}