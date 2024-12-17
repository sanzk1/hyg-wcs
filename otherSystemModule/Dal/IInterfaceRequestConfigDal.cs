using domain.Pojo.ortherSystems;

namespace otherSystemModule.Dal;

public interface IInterfaceRequestConfigDal
{
    
    void Insert(InterfaceRequestConfig config);
    
    void Update(InterfaceRequestConfig config);
    
    void Delete(InterfaceRequestConfig config);
    
    void DeleteById(List<long> ids);
    
    InterfaceRequestConfig SelectById(long id);
    
    
}