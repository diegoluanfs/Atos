namespace Report.Map.Entities
{
    public class Occurrence
    {
        public Occurrence(decimal latitude, decimal longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public Occurrence() { }

        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}
