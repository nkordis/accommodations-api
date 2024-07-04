namespace Accommodations.App.User
{
    public interface IUserContext
    {
        CurrentUser? GetCurrentUser();
    }
}