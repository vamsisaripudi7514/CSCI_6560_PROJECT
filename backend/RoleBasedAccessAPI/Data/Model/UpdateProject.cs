namespace RoleBasedAccessAPI.Data.Model
{
    public class UpdateProject
    {
        public int SourceEmployeeId { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public int ManagerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
