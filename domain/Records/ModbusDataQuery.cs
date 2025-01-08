namespace domain.Records;

public record ModbusDataQuery(string? name, string? category, string? ip, int? startAddress, int pageNum = 1, int pageSize = 10);