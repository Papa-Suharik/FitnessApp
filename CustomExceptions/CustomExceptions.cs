namespace FitnessApp.CustomExceptions;
public class UserNotFoundException(string message) : Exception(message){}
public class WrongDataProvidedException(string message) : Exception(message){}
public class DuplicateUserException(string message) : Exception(message){}

