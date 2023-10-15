using ParkingApp.Model;
using ParkingApp.Model.EntityFramework;

namespace ParkingApp.Services.Employees
{
    public interface IEmployeesService
    {
        Task<bool> BookSpot(string login, BookSpotQuery query);
        Task<bool> CancelBooking(string bookingId);
        Task<string?> CheckLogin(HttpRequest request);
        Task<List<string>?> GetAvailableSpots();
        Task<int?> GetBalance(string login);
        Task<List<Booking>?> GetBookings(string login);
    }
}
