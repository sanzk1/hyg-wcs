using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using domain.Dto;
using domain.Pojo.protocol;

namespace dataPointsModule.Managers ;

public interface IModbusManager
{
    void Connect(ModbusDataPoint t);

    DataPointDto Read(ModbusDataPoint t);

    DataPointDto Write(ModbusDataPoint t, object value);




}
