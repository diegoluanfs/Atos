namespace report.Auth.Entities
{
    public class AuthCreate
    {
		public string Name { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }
		public int IdLanguage { get; set; }
		public string Token { get; set; }
		public DateTime Created { get; set; }
		public int CreatedBy { get; set; }
		public int UpdatedBy { get; set; }
		public AuthStatus FkStatus { get; set; }
	}
}
