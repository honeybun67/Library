using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Library.Data.Models
{
    public class User:IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; } = new HashSet<IdentityUserRole<string>>();

        public virtual ICollection<AuthorRating> Ratings { get; set; } = new HashSet<AuthorRating>();
    }
}
