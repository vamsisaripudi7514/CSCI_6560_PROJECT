namespace RoleBasedAccessAPI.Data.Model
{
    public class UpdateUserPassword
    {
        public string UserName { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
