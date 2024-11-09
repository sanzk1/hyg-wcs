namespace domain.Records;

public record ProtocolLogQuery(string? name, string? category, bool? status, DateTime? startTime, DateTime? endTime, int pageNum, int pageSize);