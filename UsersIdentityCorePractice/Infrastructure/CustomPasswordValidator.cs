using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersIdentityCorePractice.Models;

namespace UsersIdentityCorePractice.Infrastructure
{
    public class CustomPasswordValidator : PasswordValidator<AppUser>
    {
        public override async Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string password)
        {
            var result = await base.ValidateAsync(manager, user, password);

            var errors = result.Succeeded ? new List<IdentityError>() : result.Errors.ToList();

            if (password.ToLower().Contains(user.UserName.ToLower()))
            {
                errors.Add(new IdentityError { Code = "PasswordContainsUsername", Description = "Password cannot contain username" });
            }

            return errors.Any() ? IdentityResult.Failed(errors.ToArray()) : IdentityResult.Success;
        }
    }
}
