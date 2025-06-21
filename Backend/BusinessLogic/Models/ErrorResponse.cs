namespace BLL.DTOs;

public record ErrorResponse(string Message, int StatusCode, IEnumerable<string>? Errors = null);