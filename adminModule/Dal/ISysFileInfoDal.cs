using domain.Pojo.sys;
using infrastructure.Utils;

namespace adminModule.Dal;

public interface ISysFileInfoDal
{
    
    
    void Insert(SysFileInfo entity);
    
    Pager<SysFileInfo> Select(int? type, int skip, int take);
    
}