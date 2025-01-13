
using domain.Dto;
using domain.Pojo.sys;

namespace adminModule.Bll
{
    public interface ISysSettingBll
    {
        
        void AddOrUpdate(SettingDto dto);
        
        SysSetting GetByKey(string key);

    }
}