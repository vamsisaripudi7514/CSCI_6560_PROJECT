namespace RoleBasedAccessAPI.Data.Model
{
    public class employee
    {
        public int EmployeeId { get; set; }  // Maps to `employee_id`
        public string EmployeeName { get; set; }  // Maps to `employee_name`
        public string Email { get; set; }  // Maps to `email`
        public string Phone { get; set; }  // Maps to `phone`
        public int RoleId { get; set; }  // Maps to `role_id`
        public int ManagerId { get; set; }  // Maps to `manager_id`
        public string EncryptedSalary { get; set; }  // Maps to `encrypted_salary`
        public DateTime HireDate { get; set; }  // Maps to `hire_date`
        public bool IsWorking { get; set; }  // Maps to `is_working`
        public DateTime CreatedAt { get; set; }  // Maps to `created_at`
        public DateTime UpdatedAt { get; set; }  // Maps to `updated_at`
    }
}
