using domain.Pojo.sys;
using infrastructure.Utils;
using Microsoft.AspNetCore.Http;

namespace adminModule.Bll;

public interface ISysFileInfoBll
{
    
    public string SaveFileInfo(IFormFile file);

    Pager<SysFileInfo> GetList(int? type, int current, int pageSize);

}