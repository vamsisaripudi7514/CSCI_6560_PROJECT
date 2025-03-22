namespace RoleBasedAccessAPI.Data.Model
{
    public class UpdateEmployee
    {
        public int SourceEmployeeId { get; set; }
        public int TargetEmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeEmail { get; set; }
        public string EmployeePhone { get; set; }
        public int EmployeeRoleId { get; set; }
        public int EmployeeManagerId { get; set; }
        public string EmployeeSalary { get; set; }
        public bool IsWorking { get; set; }
    }
}
