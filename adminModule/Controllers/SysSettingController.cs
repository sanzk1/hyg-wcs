using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using adminModule.Bll;
using domain.Result;
using Microsoft.AspNetCore.Mvc;

namespace adminModule.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SysSettingController : ControllerBase
    {

        private readonly ISysSettingBll sysSettingBll;

        public SysSettingController(ISysSettingBll sysSettingBll){
            this.sysSettingBll = sysSettingBll;
        }

        [HttpGet]
        public ApiResult test(int i){
            sysSettingBll.Add(i);
            return ApiResult.succeed();
        }


        
    }
}