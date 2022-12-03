namespace Report.Map.Entities
{
    public class Marker
    {

        public Marker() { }

        public Guid Hash { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Description { get; set; }
        public int MarkerType { get; set; }
    }
}
