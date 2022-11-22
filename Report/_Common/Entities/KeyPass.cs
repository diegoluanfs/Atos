namespace report._Common.Entities
{
    public class KeyPass
    {
        public string Key { get; set; }
        public int IdUser { get; set; }
        public string Hash { get; set; }
        public int Status { get; set; }
        public DateTime Expire { get; set; }
        public DateTime Created { get; set; }
    }
}
