using System.Text.Json.Serialization;

namespace ParkingApp.Model
{
    public class NotifyRuleQuery
    {
        private string _sendTimeStr;
        [JsonPropertyName("send_time")]
        public string SendTimeStr 
        {
            get => _sendTimeStr;    
            set
            {
                _sendTimeStr = value;
                SendTime = TimeOnly.Parse(value);
            }
        }
        [JsonPropertyName("receiver_email")]
        public string ReceiverEmail { get; set; }
        [JsonPropertyName("email_template")]
        public string EmailTemplate { get; set; }
        [JsonIgnore]
        public TimeOnly SendTime { get; private set; }
    }
}
