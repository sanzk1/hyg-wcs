
using domain.Dto;

namespace jobcoreModule.Core.Block;

public interface IS7Block
{

    /// <summary>
    /// 读取单个数据点
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    DataPointDto Read(string id, string name);
    /// <summary>
    /// 写入单个数据点
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <param name="value"></param>
    void Write(string id,string name, object value);

    /// <summary>
    /// 读取直到值等于value
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    bool WhileRead(string id,string name, object value);
    

}