using System.Text.Json.Serialization;

namespace ParkingApp.Model
{
    public class ManagerRegisterQuery
    {
        [JsonPropertyName("secret_code")]
        public string SecretCode { get; set; }
    }
}
