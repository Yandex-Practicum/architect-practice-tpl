using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ParkingApp.Model.EntityFramework
{
    public class Booking
    {
        [JsonIgnore]
        public int? Id 
        {
            get;
            set; 
           
        }
        [NotMapped]
        [JsonPropertyName("id")]
        public string? IdStr {  get => Id.ToString(); }
        [JsonPropertyName("spot_code")]
        public string SpotCode { get; set; }
        [JsonPropertyName("date")]
        public string DateStr { get => $"{Date.Year}-{Date.Month}-{Date.Day}"; }
        [JsonIgnore]
        public DateTime Date { get; set; }
        [JsonPropertyName("car_plate_number")]
        public string CarPlateNumber { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonIgnore]
        public Employee Employee { get; set; }

    }
}
