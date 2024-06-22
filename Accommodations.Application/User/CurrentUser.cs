
namespace Accommodations.App.User
{
    public record CurrentUser(string Id, string Email, IEnumerable<string> Roles)
    {
        public bool IsInRole(string roleName) => Roles.Contains(roleName);
    }
}
