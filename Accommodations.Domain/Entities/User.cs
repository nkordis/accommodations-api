using Microsoft.AspNetCore.Identity;

namespace Accommodations.Domain.Entities
{
    public class User : IdentityUser
    {
        public DateOnly? DateOfBirth { get; set; }
        public string? Nationality { get; set; }
    }
}
