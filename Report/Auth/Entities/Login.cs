namespace Report.Auth.Entities
{
    public class Login
    {
        public int Id { get; set; }
        public string Hash { get; set; }
        public string Password { get; set; }
    }
}
