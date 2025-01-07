using domain.Pojo.protocol;
using infrastructure.Attributes;
using infrastructure.Db;
using infrastructure.Utils;

namespace dataPointsModule.Dal.Impl;

[Service]
public class ModbusDataDal : IModbusDataDal
{
    private DbClientFactory _dbClientFactory => ServiceUtil.GetRequiredService<DbClientFactory>();


    public void Insert(ModbusDataPoint modbusDataPoint)
    {
        using var db = _dbClientFactory.GetSqlSugarClient();
        db.Insertable<ModbusDataPoint>(modbusDataPoint).ExecuteCommand();
    }

    public ModbusDataPoint SelectByName(string name)
    {
        using var db = _dbClientFactory.GetSqlSugarClient();
        return db.Queryable<ModbusDataPoint>().Single(x => x.name.Equals(name));
    }

    public void Delete(List<long> ids)
    {
        using var db = _dbClientFactory.GetSqlSugarClient();
        db.Deleteable<ModbusDataPoint>().In(ids);
    }

    public ModbusDataPoint SelectById(long id)
    {
        using var db = _dbClientFactory.GetSqlSugarClient();
        return db.Queryable<ModbusDataPoint>().First(x => x.id == id);
    }
    
    
}