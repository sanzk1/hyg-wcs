using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using adminModule.Bll;
using api.Common.DTO;
using domain.Pojo.sys;
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
        
        [HttpPost]
        public ApiResult addOrUpdate([FromBody]SettingDto dto){
            sysSettingBll.AddOrUpdate(dto);
            return ApiResult.succeed();
        }
        [HttpGet]
        public ApiResult get(string key){
            return ApiResult.succeed(sysSettingBll.GetByKey(key));
        }


        
    }
}