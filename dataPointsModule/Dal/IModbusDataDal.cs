using domain.Pojo.protocol;

namespace dataPointsModule.Dal;

public interface IModbusDataDal
{

    void Insert(ModbusDataPoint modbusDataPoint);
    
    ModbusDataPoint SelectByName(string name);

    void Delete(List<long> ids);

    ModbusDataPoint SelectById(long id);
    

}