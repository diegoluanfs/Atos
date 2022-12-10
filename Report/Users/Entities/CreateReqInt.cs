namespace Report.Users.Entities
{
    public class CreateReqInt
    {
        public string FullName { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
    }
}
