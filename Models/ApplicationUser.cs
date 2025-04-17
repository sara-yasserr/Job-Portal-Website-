using Microsoft.AspNetCore.Identity;

namespace Job_Portal_Project.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Address { get; set; }
        public string Company { get; set; }
    }
}
