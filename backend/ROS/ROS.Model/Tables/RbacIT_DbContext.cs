using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROS.Model.Tables
{
    public class RbacIT_DbContext : DbContext
    {
        public RbacIT_DbContext(DbContextOptions<RbacIT_DbContext> options) : base(options)
        {
        }

        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<AccessPermission> AccessPermissions { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Timesheet> Timesheets { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<EmployeeProject> EmployeeProjects { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    // Mapping for user_roles
        //    modelBuilder.Entity<UserRole>(entity =>
        //    {
        //        entity.ToTable("user_roles");
        //        entity.HasKey(e => e.RoleId);
        //        entity.Property(e => e.RoleName).IsRequired().HasMaxLength(50);
        //        entity.HasIndex(e => e.RoleName).IsUnique();
        //    });

        //    // Mapping for employees
        //    modelBuilder.Entity<Employee>(entity =>
        //    {
        //        entity.ToTable("employees");
        //        entity.HasKey(e => e.EmployeeId);
        //        entity.Property(e => e.EmployeeName).IsRequired().HasMaxLength(100);
        //        entity.Property(e => e.Email).IsRequired().HasMaxLength(150);
        //        entity.HasIndex(e => e.Email).IsUnique();
        //        entity.Property(e => e.Phone).HasMaxLength(15);
        //        entity.Property(e => e.EncryptedSalary).IsRequired();

        //        entity.HasOne(e => e.Manager)
        //              .WithMany(e => e.Subordinates)
        //              .HasForeignKey(e => e.ManagerId)
        //              .OnDelete(DeleteBehavior.SetNull);

        //        entity.HasOne(e => e.Role)
        //              .WithMany(r => r.Employees)
        //              .HasForeignKey(e => e.RoleId)
        //              .OnDelete(DeleteBehavior.Cascade);
        //    });

        //    // Mapping for access_permissions
        //    modelBuilder.Entity<AccessPermission>(entity =>
        //    {
        //        entity.ToTable("access_permissions");
        //        entity.HasKey(e => e.PermissionId);
        //        entity.Property(e => e.DbTableName).IsRequired().HasMaxLength(100);
        //        entity.Property(e => e.CanSelect).IsRequired();
        //        entity.Property(e => e.CanInsert).IsRequired();
        //        entity.Property(e => e.CanUpdate).IsRequired();
        //        entity.Property(e => e.CanDelete).IsRequired();
        //        entity.HasIndex(e => new { e.RoleId, e.DbTableName }).IsUnique();

        //        entity.HasOne(ap => ap.Role)
        //              .WithMany(r => r.AccessPermissions)
        //              .HasForeignKey(ap => ap.RoleId)
        //              .OnDelete(DeleteBehavior.Cascade);
        //    });

        //    // Mapping for projects
        //    modelBuilder.Entity<Project>(entity =>
        //    {
        //        entity.ToTable("projects");
        //        entity.HasKey(e => e.ProjectId);
        //        entity.Property(e => e.ProjectName).IsRequired().HasMaxLength(200);

        //        entity.HasOne(p => p.Manager)
        //              .WithMany(e => e.ManagedProjects)
        //              .HasForeignKey(p => p.ManagerId)
        //              .OnDelete(DeleteBehavior.Cascade);
        //    });

        //    // Mapping for timesheets
        //    modelBuilder.Entity<Timesheet>(entity =>
        //    {
        //        entity.ToTable("timesheets");
        //        entity.HasKey(e => e.TimesheetId);
        //        entity.Property(e => e.HoursLogged).IsRequired().HasColumnType("decimal(5,2)");
        //        entity.Property(e => e.LogDate).IsRequired();

        //        entity.HasOne(t => t.Employee)
        //              .WithMany(e => e.Timesheets)
        //              .HasForeignKey(t => t.EmployeeId)
        //              .OnDelete(DeleteBehavior.Cascade);

        //        entity.HasOne(t => t.Project)
        //              .WithMany(p => p.Timesheets)
        //              .HasForeignKey(t => t.ProjectId)
        //              .OnDelete(DeleteBehavior.Cascade);
        //    });

        //    // Mapping for audit_logs
        //    modelBuilder.Entity<AuditLog>(entity =>
        //    {
        //        entity.ToTable("audit_logs");
        //        entity.HasKey(e => e.LogId);
        //        entity.Property(e => e.DbAction).IsRequired().HasMaxLength(255);
        //        entity.Property(e => e.DbTableName).HasMaxLength(100);

        //        entity.HasOne(al => al.Employee)
        //              .WithMany(e => e.AuditLogs)
        //              .HasForeignKey(al => al.UserId)
        //              .OnDelete(DeleteBehavior.Cascade);
        //    });

        //    // Mapping for users
        //    modelBuilder.Entity<User>(entity =>
        //    {
        //        entity.ToTable("users");
        //        entity.HasKey(u => u.EmployeeId);
        //        entity.Property(u => u.Username).IsRequired().HasMaxLength(50);
        //        entity.HasIndex(u => u.Username).IsUnique();
        //        entity.Property(u => u.UserPassword).IsRequired();

        //        entity.HasOne(u => u.Employee)
        //              .WithOne(e => e.User)
        //              .HasForeignKey<User>(u => u.EmployeeId)
        //              .OnDelete(DeleteBehavior.Cascade);
        //    });

        //    // Mapping for employee_projects (many-to-many join table)
        //    modelBuilder.Entity<EmployeeProject>(entity =>
        //    {
        //        entity.ToTable("employee_projects");
        //        entity.HasKey(ep => new { ep.EmployeeId, ep.ProjectId });

        //        entity.HasOne(ep => ep.Employee)
        //              .WithMany(e => e.EmployeeProjects)
        //              .HasForeignKey(ep => ep.EmployeeId)
        //              .OnDelete(DeleteBehavior.Cascade);

        //        entity.HasOne(ep => ep.Project)
        //              .WithMany(p => p.EmployeeProjects)
        //              .HasForeignKey(ep => ep.ProjectId)
        //              .OnDelete(DeleteBehavior.Cascade);
        //    });

        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
