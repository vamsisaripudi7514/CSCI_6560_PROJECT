using ROS.Model.Tables;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class AuditLog
{
    [Key]
    public int LogId { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    [MaxLength(255)]
    public string DbAction { get; set; }

    [MaxLength(100)]
    public string DbTableName { get; set; }

    public int? RecordId { get; set; }

    public DateTime DbTimestamp { get; set; } = DateTime.Now;

    // Navigation property (points to the employee who performed the action)
    [ForeignKey("UserId")]
    public virtual Employee Employee { get; set; }
}