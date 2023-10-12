namespace ParkingApp.Model.EntityFramework
{
    public class EmailNotify
    {
        public int? Id {  get; set; }
        public TimeOnly SendTime {  get; set; }
        public  string RecieverEmail { get; set; }
        public string EmailTemplate { get; set; }
    }
}
