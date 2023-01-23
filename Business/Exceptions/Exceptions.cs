using System.Net;
using System.Runtime.Serialization;

namespace Application.Exceptions;

public class ErrorMessage
{
    public string? Error { get; set; }
}
public abstract class CustomException : Exception
{
    protected CustomException(string message) : base(message) { }
    public abstract int StatusCode { get; }
    public abstract object Error { get; }
}

public class ValidationException : CustomException
{
    public ValidationException(string message) : base(message) { }
    public override int StatusCode => (int)HttpStatusCode.BadRequest;
    public override object Error => new ErrorMessage { Error = Message };
}

public class NotFoundException : CustomException
{
    public NotFoundException(string message) : base(message) { }
    public override int StatusCode => (int)HttpStatusCode.NotFound;
    public override object Error => new ErrorMessage { Error = Message };
}

public class UnauthorizedException : CustomException
{
    public UnauthorizedException(string message) : base(message) { }
    public override int StatusCode => (int)HttpStatusCode.Unauthorized;
    public override object Error => new ErrorMessage { Error = Message };
}


public class ForbiddenException : CustomException
{
    public ForbiddenException(string message) : base(message) { }
    public override int StatusCode => (int)HttpStatusCode.Forbidden;
    public override object Error => new ErrorMessage { Error = Message };
}

public class UnsoportedMediaTypeException : CustomException
{
    public UnsoportedMediaTypeException(string message) : base(message) { }
    public override int StatusCode => (int)HttpStatusCode.UnsupportedMediaType;
    public override object Error => new ErrorMessage { Error = Message };
}

