using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROS.Model.Tables
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        [MaxLength(100)]
        public string EmployeeName { get; set; }

        [Required]
        [MaxLength(150)]
        public string Email { get; set; }

        [MaxLength(15)]
        public string Phone { get; set; }

        [Required]
        public int RoleId { get; set; }

        public int? ManagerId { get; set; }

        [Required]
        public byte[] EncryptedSalary { get; set; }

        public DateTime? HireDate { get; set; }

        [Required]
        public bool IsWorking { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Navigation properties
        [ForeignKey("RoleId")]
        public virtual UserRole Role { get; set; }

        [ForeignKey("ManagerId")]
        public virtual Employee Manager { get; set; }

        public virtual ICollection<Employee> Subordinates { get; set; } = new List<Employee>();

        public virtual ICollection<Timesheet> Timesheets { get; set; } = new List<Timesheet>();

        public virtual ICollection<Project> ManagedProjects { get; set; } = new List<Project>();

        public virtual ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();

        public virtual User User { get; set; }

        public virtual ICollection<EmployeeProject> EmployeeProjects { get; set; } = new List<EmployeeProject>();

    }
}
