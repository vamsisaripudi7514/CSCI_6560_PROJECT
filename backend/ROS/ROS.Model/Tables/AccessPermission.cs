using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROS.Model.Tables
{
    public class AccessPermission
    {
        [Key]
        public int PermissionId { get; set; }

        [Required]
        public int RoleId { get; set; }

        [Required]
        [MaxLength(100)]
        public string DbTableName { get; set; }

        [Required]
        public bool CanSelect { get; set; }

        [Required]
        public bool CanInsert { get; set; }

        [Required]
        public bool CanUpdate { get; set; }

        [Required]
        public bool CanDelete { get; set; }

        // Navigation property
        [ForeignKey("RoleId")]
        public virtual UserRole Role { get; set; }
    }
}
