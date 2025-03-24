namespace RoleBasedAccessAPI.Data.Model
{
    public class UpdateProjectMapping
    {
        public int SourceEmployeeId { get; set; }
        public int TargetEmployeeId { get; set; }
        public int ProjectId { get; set; }

        public int OldProjectId { get; set; }
    }
}
