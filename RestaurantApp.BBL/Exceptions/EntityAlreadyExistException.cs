namespace RestaurantApp.BBL.Exceptions;

public class EntityAlreadyExistException : Exception
{
    public EntityAlreadyExistException(string message) : base(message)
    {

    }
}