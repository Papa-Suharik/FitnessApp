namespace FitnessApp.CustomExceptions;

public abstract class DomainException : Exception
{
    public abstract int StatusCode {get;}
}
public class UserNotFoundException(string message) : DomainException
{
    public override int StatusCode => StatusCodes.Status404NotFound;
    public override string Message => message;
}
public class WrongDataProvidedException(string message) : DomainException
{
    public override int StatusCode => StatusCodes.Status400BadRequest;
    public override string Message => message;
}
public class DuplicateUserException(string message) : DomainException
{
    public override int StatusCode => StatusCodes.Status400BadRequest;
    public override string Message => message;
}

