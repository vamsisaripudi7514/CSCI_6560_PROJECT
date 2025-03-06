using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROS.Model.Tables
{
    public class Timesheet
    {
        [Key]
        public int TimesheetId { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [Required]
        [Column(TypeName = "decimal(5,2)")]
        public decimal HoursLogged { get; set; }

        [Required]
        public DateTime LogDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Navigation properties
        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }

        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }
    }
}
