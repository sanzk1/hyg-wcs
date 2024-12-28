using domain.Pojo.ortherSystems;
using domain.Result;

namespace otherSystemModule.Bll;

public interface IInterfaceRequestConfigBll
{
    
    
    
    public ApiResult Execute(long configId, object param);
    
    
}