namespace Report.Map.Entities
{
    public class Occurrence
    {

        public Occurrence() { }

        public Occurrence(string latitude, string longitude, int occurrenceType, string occurrenceDescription)
        {
            Latitude = latitude;
            Longitude = longitude;
            OccurrenceType = occurrenceType;
            OccurrenceDescription = occurrenceDescription;
        }

        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int OccurrenceType { get; set; }
        public string OccurrenceDescription { get; set; }
    }
}
