using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using adminModule.Dal;
using domain.Pojo.sys;
using infrastructure.Attributes;
using infrastructure.Exceptions;
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
        public void Add(int i)
        {
            SysSetting sysSetting = new();
            sysSetting.id= YitIdHelper.NextId();
            sysSetting.keyName = "ces";
            sysSetting.value = "12";
            sysSettingDal.Insert(sysSetting);
            if(i == 1)
                throw new BusinessException("测试事务异常回滚");
            
        }
    }
}