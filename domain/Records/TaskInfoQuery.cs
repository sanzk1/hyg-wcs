namespace domain.Records;

public record TaskInfoQuery(string? name, int? type, int?  executeStatus, int? interrupt, DateTime? startTime, DateTime? endTime, int pageNum, int pageSize);