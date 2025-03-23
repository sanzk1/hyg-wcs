namespace domain.Records;

public record ProtocolLogQuery(string? name, string? category, bool? status, string? oper, DateTime? startTime, DateTime? endTime, int pageNum, int pageSize);