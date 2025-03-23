using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using otherSystemModule.Bll;

namespace otherSystemModule.Controller;


[Authorize]
[Route("[controller]/[action]")]
[ApiController]
public class InterfaceRequestConfigController : ControllerBase
{
    private readonly IInterfaceRequestConfigBll _interfaceRequestConfigBll;

    public InterfaceRequestConfigController(IInterfaceRequestConfigBll interfaceRequestConfigBll)
    {
        _interfaceRequestConfigBll = interfaceRequestConfigBll;
    }
    
    
    
}