using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ParkingApp.Model.EntityFramework
{
    public class Booking
    {
        private int? id = null!;
        [JsonIgnore]
        public int? Id 
        { 
            get => id; 
            set 
            {
                IdStr = value.ToString();
                id = value; 
            } 
        }
        [NotMapped]
        [JsonPropertyName("id")]
        public string? IdStr {  get; set; }
        [NotMapped]
        [JsonPropertyName("spot_code")]
        public string SpotCode { get; set; }
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }
        [JsonPropertyName("car_plate_number")]
        public string CarPlateNumber { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonIgnore]
        public Employee Employee { get; set; }

    }
}
