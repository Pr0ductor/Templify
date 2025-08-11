namespace Templify.Shared.Exceptions;

public class AppException : Exception
{
    public AppException() : base() { }
    
    public AppException(string message) : base(message) { }
    
    public AppException(string message, Exception innerException) : base(message, innerException) { }
}

public class ValidationException : AppException
{
    public ValidationException(string message) : base(message) { }
}

public class NotFoundException : AppException
{
    public NotFoundException(string name, object key) : base($"Entity \"{name}\" ({key}) was not found.") { }
}

public class UnauthorizedException : AppException
{
    public UnauthorizedException(string message = "Unauthorized") : base(message) { }
}
