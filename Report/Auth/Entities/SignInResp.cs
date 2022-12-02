namespace Report.Auth.Entities
{
    public class SignInResp
    {
        public string Authorization { get; set; }
        public SignInAuthResp Auth { get; set; }
    }
}
