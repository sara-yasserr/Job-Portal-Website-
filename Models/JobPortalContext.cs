using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Job_Portal_Project.Models
{
    public class JobPortalContext : IdentityDbContext<ApplicationUser>
    {

        public JobPortalContext(DbContextOptions<JobPortalContext> options):base(options) { }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog= JobPortalDB;Integrated Security=True;Connect Timeout=30;Trust Server Certificate=True;");
        //}
    }
}
