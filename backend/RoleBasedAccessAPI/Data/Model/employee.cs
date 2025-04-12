namespace RoleBasedAccessAPI.Data.Model
{
    public class employee
    {
        public int EmployeeId { get; set; }  
        public string EmployeeName { get; set; } 
        public string Email { get; set; }  
        public string Phone { get; set; }  
        public int RoleId { get; set; }  
        public int ManagerId { get; set; }  
        public string EncryptedSalary { get; set; }  
        public DateTime HireDate { get; set; } 
        public bool IsWorking { get; set; } 
        public DateTime CreatedAt { get; set; } 
        public DateTime UpdatedAt { get; set; }  
    }

}
