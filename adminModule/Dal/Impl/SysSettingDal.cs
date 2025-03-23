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
        private DbClientFactory dbClientFactory => ServiceUtil.GetRequiredService<DbClientFactory>();

        

        public void Insert(SysSetting sysSetting)
        {
            using var db = dbClientFactory.GetSqlSugarClient();
            db.Insertable<SysSetting>(sysSetting).ExecuteCommand();

        }

        public void Update(SysSetting sysSetting)
        {
            using var db = dbClientFactory.GetSqlSugarClient();
            db.Updateable<SysSetting>(sysSetting).ExecuteCommand();
        }

        public SysSetting SelectByName(string keyName)
        {
            using var db = dbClientFactory.GetSqlSugarClient();
            return db.Queryable<SysSetting>().Single( s => s.keyName.Equals(keyName));
        }

        public SysSetting SelectByKey(string keyName)
        {
            using var db = dbClientFactory.GetSqlSugarClient();
            return db.Queryable<SysSetting>().Single( s => s.keyName.Equals(keyName));
        }
        
    }

}