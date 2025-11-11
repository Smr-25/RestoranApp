namespace RestaurantApp.BBL.Exceptions;

public class MenuItemAlreadyExistException : Exception
{
    public MenuItemAlreadyExistException(string message) : base(message)
    {

    }
}