using System.Text.Json.Serialization;

namespace ParkingApp.Model
{
    public class BookSpotQuery
    {
        [JsonPropertyName("spot_code")]
        public string SpotCode { get; set; }
        [JsonPropertyName("car_plate_number")]
        public string CarPlateNumber { get; set;}
    }
}
