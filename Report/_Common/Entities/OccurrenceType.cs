namespace Report._Common.Entities
{
    public class OccurrenceType
    {

        public OccurrenceType() { }

        public OccurrenceType(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
