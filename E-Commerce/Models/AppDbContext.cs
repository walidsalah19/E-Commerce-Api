using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Models
{
    public class AppDbContext : IdentityDbContext<UserApplication>
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
         //   modelBuilder.ApplyConfigurationsFromAssembly(typeof(DepartmentConfige).Assembly);
        }
    }
}
