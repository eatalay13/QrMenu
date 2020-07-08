using Microsoft.AspNetCore.Identity;

namespace Entities.Models.Auth
{
    public class User : IdentityUser<int>
    {
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}
