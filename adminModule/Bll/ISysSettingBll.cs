using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Common.DTO;
using domain.Pojo.sys;

namespace adminModule.Bll
{
    public interface ISysSettingBll
    {
        
        void AddOrUpdate(SettingDto dto);
        
        SysSetting GetByKey(string key);

    }
}