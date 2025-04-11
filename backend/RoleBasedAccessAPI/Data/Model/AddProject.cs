namespace RoleBasedAccessAPI.Data.Model
{
    public class AddProject
    {
        public int employee_id { get; set; }
        public int project_id { get; set; }
        public string project_name { get; set; }
        public string project_description { get; set; }
        public int manager_id { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }

    }
}
