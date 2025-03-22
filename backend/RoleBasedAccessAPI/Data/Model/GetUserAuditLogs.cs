namespace RoleBasedAccessAPI.Data.Model
{
    public class GetUserAuditLogs
    {
        public int employeeID { get; set; }  // Maps to `employee_id`
        public int searchID { get; set; }  // Maps to `search_id`
    }
}
