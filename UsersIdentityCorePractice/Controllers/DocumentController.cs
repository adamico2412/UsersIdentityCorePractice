using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UsersIdentityCorePractice.Models;

namespace UsersIdentityCorePractice.Controllers
{
    [Authorize]
    public class DocumentController : Controller
    {
        private ProtectedDocument[] docs = new ProtectedDocument[]
        {
            new ProtectedDocument { Title = "Q3 Budget", Author = "Alice", Editor = "Joe" },
            new ProtectedDocument { Title = "Project Plan", Author = "Bob", Editor = "Alice" }
        };

        private IAuthorizationService _authService;

        public DocumentController(IAuthorizationService authService)
        {
            _authService = authService;
        }

        public IActionResult Index() => View(docs);

        public async Task<IActionResult> Edit(string title)
        {
            var doc = docs.FirstOrDefault(d => d.Title == title);
            var authorized = await _authService.AuthorizeAsync(User, doc, "AuthorsAndEditors");

            if (authorized.Succeeded)
            {
                return View("Index", doc);
            }
            else
            {
                return new ChallengeResult();
            }
        }
    }
}