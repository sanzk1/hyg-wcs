using domain.Pojo.sys;
using Microsoft.AspNetCore.Http;

namespace adminModule.Bll;

public interface ISysFileInfoBll
{
    
    public string SaveFileInfo(IFormFile file);
    
}