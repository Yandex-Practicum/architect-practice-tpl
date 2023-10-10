namespace ParkingApp.Model.EntityFramework
{
    public class Booking
    {
        public int Id { get; set; }
        public string SpotCode { get; set; }
        public DateTime Date { get; set; }
        public string CarPlateNumber { get; set; }
        public string Status { get; set; }
        public Employee Employee { get; set; }

    }
}
