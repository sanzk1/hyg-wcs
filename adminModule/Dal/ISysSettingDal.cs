using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using domain.Pojo.sys;

namespace adminModule.Dal
{
    public interface ISysSettingDal
    {

        void Insert(SysSetting sysSetting);
        
    }

}