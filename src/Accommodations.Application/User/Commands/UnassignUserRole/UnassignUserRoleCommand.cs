using MediatR;

namespace Accommodations.App.User.Commands.UnassignUserRole
{
    public class UnassignUserRoleCommand : IRequest
    {
        public string UserEmail { get; set; } = default!;
        public string RoleName { get; set; } = default!;
    }
}
