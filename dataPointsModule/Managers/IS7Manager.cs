using api.Common.DTO;
using domain.Pojo.protocol;

namespace dataPointsModule.Managers;

public interface IS7Manager
{
  
    /// <summary>
    /// 单个数据点读取
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public DataPointDto Read(S7DataPoint point);

    /// <summary>
    /// 单个数据点写入
    /// </summary>
    /// <param name="point"></param>
    /// <param name="obj"></param>
    /// <returns></returns>
    public DataPointDto Write(S7DataPoint point, object obj);

    public void Connect(S7DataPoint point);

}