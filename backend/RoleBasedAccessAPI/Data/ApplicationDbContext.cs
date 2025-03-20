using Microsoft.EntityFrameworkCore;
using RoleBasedAccessAPI.Data.Model;

namespace RoleBasedAccessAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Define DbSet for Employee model (following your lowercase naming)
        public DbSet<employee> Employees { get; set; }

        // Define DbSet for UserLoginDto (if needed for EF Core, otherwise it's just for DTO purposes)
        public DbSet<UserLoginDto> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Prevent EF Core from expecting migrations for DTOs
            modelBuilder.Entity<UserLoginDto>().HasNoKey().ToTable("Users", t => t.ExcludeFromMigrations());
            modelBuilder.Entity<employee>().HasNoKey().ToTable("Employees", t => t.ExcludeFromMigrations());
        }
    }
}




















//using Microsoft.EntityFrameworkCore;

//namespace RoleBasedAccessAPI.Data
//{
//    public class ApplicationDbContext : DbContext
//    {
//        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            base.OnModelCreating(modelBuilder);
//            // Prevents EF Core from expecting migrations
//        }
//    }
//}

