using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using domain.Pojo.sys;
using infrastructure.Attributes;
using infrastructure.Db;
using infrastructure.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace adminModule.Dal.Impl
{
    [Service(ServiceLifetime.Singleton)]
    public class SysSettingDal : ISysSettingDal
    {
        private DbClientFactory dbClientFactory => ServiceUtil.ServiceProvider.GetRequiredService<DbClientFactory>();

        

        public void Insert(SysSetting sysSetting)
        {
            using var db = dbClientFactory.GetSqlSugarClient();
            db.Insertable<SysSetting>(sysSetting).ExecuteCommand();

        }
    }

}