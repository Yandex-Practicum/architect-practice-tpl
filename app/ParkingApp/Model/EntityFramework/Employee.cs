namespace ParkingApp.Model.EntityFramework
{
    public class Employee
    {
        public int? Id { get; set; }
        public string Login { get; set; }
        public int Balance { get; set; }
        public int MonthlyLimit { get; set; }
        public List<Booking> Bookings { get; set; } = new();
    }
}
