namespace report.Users.Entities
{
    public class UserCreate
    {
		public string TxName { get; set; }
		public string TxPassword { get; set; }
		public string TxEmail { get; set; }
		public int IdLanguage { get; set; }
		public string TxToken { get; set; }
		public DateTime DtCreated { get; set; }
		public int IdCreatedBy { get; set; }
		public int IdUpdatedBy { get; set; }
		public UserStatus IdStatus { get; set; }
	}
}
