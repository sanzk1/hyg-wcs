using domain.Pojo.protocol;
using infrastructure.Utils;

namespace dataPointsModule.Bll;

public interface IModbusDataBll
{
    /// <summary>
    /// 保存数据点
    /// </summary>
    /// <param name="modbusDataPoint"></param>
    public void Save(ModbusDataPoint modbusDataPoint);

    /// <summary>
    /// 删除数据点
    /// </summary>
    /// <param name="ids"></param>
    public void Remove(List<long> ids);

    
    public Pager<ModbusDataPoint> Find();



}