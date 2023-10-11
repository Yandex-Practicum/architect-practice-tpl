namespace ParkingApp.Model.EntityFramework
{
    public class Spot
    {
        public int? Id { get; set; }
        public string SpotCode { get; set; }
        public int? BookingId { get; set; }
        public Booking? Booking { get; set; }
    }
}
