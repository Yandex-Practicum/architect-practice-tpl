using System.Text.Json.Serialization;

namespace ParkingApp.Model
{
    public class AddLimitQuery
    {
        [JsonPropertyName("limit")]
        public int Limit { get; set; }
    }
}
