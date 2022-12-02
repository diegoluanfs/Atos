namespace Report.Auth.Entities
{
    public class ChangePasswordReq
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
