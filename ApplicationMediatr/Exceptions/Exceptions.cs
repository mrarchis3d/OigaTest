using FluentValidation.Results;
using System.Net;
using System.Text;

namespace ApplicationMediatr.Exceptions
{
    public class ErrorMessage
    {
        public string? Error { get; set; }
    }
    public abstract class CustomException : Exception
    {
        protected CustomException(string message) : base(message) { }
        public abstract int StatusCode { get; }
        public abstract ErrorMessage Error { get; }
    }

    public class ValidationException : CustomException
    {
        private readonly string? innerMessage;
        public ValidationException(string message, IEnumerable<ValidationFailure> failures): base(message)
        {
            StringBuilder sb= new StringBuilder();
            sb.AppendLine(message);
            foreach (ValidationFailure failure in failures)
            {
                sb.AppendLine(failure.ErrorMessage);
            }
            innerMessage = sb.ToString();
        }
        public ValidationException(string? message) : base(message!) { }
        public override int StatusCode => (int)HttpStatusCode.BadRequest;
        public override ErrorMessage Error => new ErrorMessage { Error = innerMessage };
    }
    public class NotFoundException : CustomException
    {
        public NotFoundException(string message) : base(message) { }
        public override int StatusCode => (int)HttpStatusCode.NotFound;
        public override ErrorMessage Error => new ErrorMessage { Error = Message };
    }

    public class UnauthorizedException : CustomException
    {
        public UnauthorizedException(string message) : base(message) { }
        public override int StatusCode => (int)HttpStatusCode.Unauthorized;
        public override ErrorMessage Error => new ErrorMessage { Error = Message };
    }


    public class ForbiddenException : CustomException
    {
        public ForbiddenException(string message) : base(message) { }
        public override int StatusCode => (int)HttpStatusCode.Forbidden;
        public override ErrorMessage Error => new ErrorMessage { Error = Message };
    }

    public class UnsoportedMediaTypeException : CustomException
    {
        public UnsoportedMediaTypeException(string message) : base(message) { }
        public override int StatusCode => (int)HttpStatusCode.UnsupportedMediaType;
        public override ErrorMessage Error => new ErrorMessage { Error = Message };
    }
}
