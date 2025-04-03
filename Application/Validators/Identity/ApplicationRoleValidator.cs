using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Application.Common.Validators
{
    public class ApplicationRoleValidator : RoleValidator<ApplicationRole>
    {
        private IdentityErrorDescriber Describer { get; set; } = new();

        public override async Task<IdentityResult> ValidateAsync(RoleManager<ApplicationRole> manager, ApplicationRole role)
        {
            //if (manager == null)
            //{
            //    throw new ArgumentNullException(nameof(manager));
            //}
            //if (role == null)
            //{
            //    throw new ArgumentNullException(nameof(role));
            //}
            //var errors = new List<IdentityError>();
            //await ValidateRoleName(manager, role, errors);
            //if (errors.Count > 0)
            //{
            //    return IdentityResult.Failed(errors.ToArray());
            //}

            await Task.Delay(0);

            return IdentityResult.Success;
        }

        private async Task ValidateRoleName(RoleManager<ApplicationRole> manager, ApplicationRole role, ICollection<IdentityError> errors)
        {
            string? roleName = await manager.GetRoleNameAsync(role);

            if (string.IsNullOrWhiteSpace(roleName))
            {
                errors.Add(Describer.InvalidRoleName(roleName));
            }
            else
            {
                ApplicationRole? owner = await manager.FindByNameAsync(roleName);

                if (owner == null)
                {
                    errors.Add(Describer.InvalidRoleName(roleName));
                }
            }
        }
    }
}