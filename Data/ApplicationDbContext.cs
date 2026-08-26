using APPR_PART_1_POE.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace APPR_PART_1_POE.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Volunteer> Volunteers { get; set; }

        public DbSet<Donation> Donations { get; set; }

        public DbSet<ReliefOperation> ReliefOperations { get; set; }
    }
}