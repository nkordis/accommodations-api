﻿
namespace Accommodations.App.User
{
    public record CurrentUser(string Id, string Email, IEnumerable<string> Roles, 
        string? Nationality, DateOnly? DateOfBirth)
    {
        public bool IsInRole(string roleName) => Roles.Contains(roleName);
    }
}
