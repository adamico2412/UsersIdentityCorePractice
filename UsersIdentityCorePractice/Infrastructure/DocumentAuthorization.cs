using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersIdentityCorePractice.Models;

namespace UsersIdentityCorePractice.Infrastructure
{
    public class DocumentAuthorizationRequirement : IAuthorizationRequirement
    {
        public bool AllowAuthors { get; set; }
        public bool AllowEditors { get; set; }
    }

    public class DocumentAuthorizationHandler : AuthorizationHandler<DocumentAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DocumentAuthorizationRequirement requirement)
        {
            var doc = context.Resource as ProtectedDocument;
            var user = context.User.Identity.Name;
            var compare = StringComparison.OrdinalIgnoreCase;

            if (doc != null && user != null 
                && (requirement.AllowAuthors && doc.Author.Equals(user, compare)) || (requirement.AllowEditors && doc.Editor.Equals(user, compare)))
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}
