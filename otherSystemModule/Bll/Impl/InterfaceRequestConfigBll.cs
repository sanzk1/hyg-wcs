using infrastructure.Attributes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using otherSystemModule.Dal;

namespace otherSystemModule.Bll.Impl;


[Service(ServiceLifetime.Singleton)]
public class InterfaceRequestConfigBll : IInterfaceRequestConfigBll
{
    private readonly IInterfaceRequestConfigDal _interfaceRequestConfigDal;
    private readonly ILogger<IInterfaceRequestConfigBll> _logger;

    public InterfaceRequestConfigBll(IInterfaceRequestConfigDal interfaceRequestConfigDal, ILogger<IInterfaceRequestConfigBll> logger)
    {
        _interfaceRequestConfigDal = interfaceRequestConfigDal;
        _logger = logger;
        
    }
    
    
    
}