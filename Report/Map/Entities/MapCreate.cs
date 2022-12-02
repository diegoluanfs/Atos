using Microsoft.Spatial;

namespace Report.Map.Entities
{
    public class MapCreate
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public Geography? Geolocation { get; set; }
    }
}
