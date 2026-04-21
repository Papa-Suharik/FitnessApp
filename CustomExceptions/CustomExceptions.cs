namespace FitnessApp.CustomExceptions;

public abstract class DomainException : Exception
{
    public DomainException(string message) : base(message){}
    public abstract int StatusCode {get;}
    public abstract string Title {get;}
}

public class UserNotFoundException : DomainException
{
    public UserNotFoundException(string message) : base(message){}
    public override int StatusCode => StatusCodes.Status404NotFound;
    public override string Title => "User is not found";
}
public class WrongDataProvidedException : DomainException
{
    public WrongDataProvidedException(string message) : base(message){}
    public override int StatusCode => StatusCodes.Status400BadRequest;
    public override string Title => "Wrong data provided";

}
public class DuplicateUserException : DomainException
{
    public DuplicateUserException(string message) : base(message){}
    public override int StatusCode => StatusCodes.Status400BadRequest;
    public override string Title => "User already exists";
}

