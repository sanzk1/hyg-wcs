namespace domain.Records;

public record OpcUaDataPointQuery(string? name,  string? category, string? ip, string? identifier, int pageNum , int pageSize);