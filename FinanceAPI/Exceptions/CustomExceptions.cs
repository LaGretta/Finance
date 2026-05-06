namespace FinanceAPI.Exceptions;

public class CustomExceptions
{
    public class NotFoundExeption : Exception
    {
        public NotFoundExeption(string message) : base(message) {}
    }

    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message) {}
    }

    public class ConflictException : Exception
    {
        public ConflictException(string message) : base(message) {}
    }

    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message) : base(message) {}
    }

}