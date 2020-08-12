using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersIdentityCorePractice.Models
{
    public class AppUser : IdentityUser
    {
        public City City { get; set; }
        public QualificationLevel Qualification { get; set; }
    }

    public enum City
    {
        None,
        London,
        Paris,
        Chicago
    }

    public enum QualificationLevel
    {
        None,
        Basic,
        Advanced
    }
}
