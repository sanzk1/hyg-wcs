namespace domain.Records;

public record S7DataPointQuery(string? name, string? category, string? ip, int? startAddress, int pageNum , int pageSize);