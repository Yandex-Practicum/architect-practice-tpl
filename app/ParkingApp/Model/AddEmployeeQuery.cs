using System.Text.Json.Serialization;

namespace ParkingApp.Model
{
    public class AddEmployeeQuery
    {
        [JsonPropertyName("login")]
        public string Login { get; set; }
    }
}
