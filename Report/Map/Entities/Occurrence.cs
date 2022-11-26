namespace Report.Map.Entities
{
    public class Occurrence
    {
        public Occurrence(string latitude, string longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
        public Occurrence() { }

        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
