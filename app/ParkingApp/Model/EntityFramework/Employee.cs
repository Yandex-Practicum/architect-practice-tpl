using System.Text.Json.Serialization;

namespace ParkingApp.Model.EntityFramework
{
    public class Employee
    {
        [JsonIgnore]
        public int? Id { get; set; }
        [JsonIgnore]
        public string Login { get; set; }
        [JsonPropertyName("balance")]
        public int Balance { get; set; } = 10;
        [JsonPropertyName("monthly_limit")]
        public int MonthlyLimit { get; set; } = 10;
        [JsonPropertyName("bookings")]
        public List<Booking>? Bookings { get; set; } = new();
    }
}
