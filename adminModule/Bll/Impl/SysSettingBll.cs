using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using adminModule.Dal;
using api.Common.DTO;
using domain.Pojo.sys;
using infrastructure.Attributes;
using Microsoft.Extensions.DependencyInjection;
using Yitter.IdGenerator;

namespace adminModule.Bll.Impl
{

    [Service(ServiceLifetime.Singleton)]
    public class SysSettingBll : ISysSettingBll
    {
        private readonly ISysSettingDal sysSettingDal;

        public SysSettingBll(ISysSettingDal sysSettingDal)
        {
            this.sysSettingDal = sysSettingDal;
        }


        [TransactionScope(TransactionScopeOption.Required)]
        public void AddOrUpdate(SettingDto dto)
        {
            SysSetting setting = sysSettingDal.SelectByName(dto.key);
            if (setting != null)
            {
                setting.keyName = dto.key;
                setting.value = dto.value;
                sysSettingDal.Update(setting);
                return;
            }
            SysSetting sysSetting = new();
            sysSetting.id= YitIdHelper.NextId();
            sysSetting.keyName = dto.key;
            sysSetting.value = dto.value;
            sysSettingDal.Insert(sysSetting);
            
        }

        public SysSetting GetByKey(string key)
        {
            return sysSettingDal.SelectByKey(key);
        }
    }
}