using Microsoft.EntityFrameworkCore;
using ParkingApp.Model;
using ParkingApp.Model.EntityFramework;
using System.Diagnostics.Eventing.Reader;

namespace ParkingApp.Services.Employees
{
    public class EmployeesService: IEmployeesService
    {
        private readonly ILogger<EmployeesService> _logger;

        public EmployeesService(ILogger<EmployeesService> logger) 
        {
            _logger = logger;
        }

        public async Task<string?> CheckLogin(HttpRequest request)
        {
            string result = null!;
            try
            {
                if (request.Headers.TryGetValue("X-Employee-Login", out var login))
                {
                    if (!string.IsNullOrEmpty(login))
                    {
                        using var db = new ParkingContext();
                        var res = db.Employees.FirstOrDefault(e => e.Login == login.First());
                        if (res != null)
                            result = login;
                    }   
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
            return result;
        }

        public async Task<int?> GetBalance(string login)
        {
            int result = 0;
            try
            {
                using var db = new ParkingContext();
                var res = db.Employees.FirstOrDefault(e => e.Login.Equals(login));
                if (res != null)
                {
                    result = res.Balance;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
            return result;
        }
        
        public async Task<List<string>?> GetAvailableSpots()
        {
            List<string> result = new();
            try
            {
                await ClearOldBookings();
                using var db = new ParkingContext();
                var res = db.Spots.Where(s => s.BookingId == null).Select(s => s.SpotCode).ToList();
                if (res != null)
                    result = res;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message);
                return null;
            }
            return result;
        }

        public async Task<bool> BookSpot(string login, BookSpotQuery query)
        {
            bool result = false;
            try
            {
                await ClearOldBookings();
                using var db = new ParkingContext();
                var emp = db.Employees.FirstOrDefault(e => e.Login == login);
                var spot = db.Spots.FirstOrDefault(s => s.SpotCode == query.SpotCode);
                if (emp != null && spot != null && spot.BookingId == null)
                {
                    var book = new Booking() { CarPlateNumber = query.CarPlateNumber, Date = DateTime.Now + TimeSpan.FromDays(1), Employee = emp, SpotCode = query.SpotCode, Status = "booked" };
                    spot.Booking = book;
                    var res = await db.SaveChangesAsync();
                    if (res > 0)
                        result = true;
                }      
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return false;
            }
            return result;
        }

        public async Task<List<Booking>?> GetBookings(string login)
        {
            List<Booking>? result = new();
            try
            {
                using var db = new ParkingContext();
                var res = db.Bookings.Include(e => e.Employee).Where(b => b.Employee.Login == login).ToList();
                if (res is not null)
                    result = res;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
            return result;
        }

        public async Task<bool> CancelBooking(string bookingId)
        {
            bool result = false;
            try
            {
                using var db = new ParkingContext();
                var booking = db.Bookings.Where(b => b.Id.ToString() == bookingId).FirstOrDefault();
                if (booking != null)
                {
                    var spot = db.Spots.FirstOrDefault(s => s.SpotCode==booking.SpotCode);
                    if (spot != null)
                    {
                        booking.Status = "canceled";
                        spot.Booking = null;
                        db.SaveChanges();
                        result = true;
                    }
                }
                else
                    result = false;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message);
                return false;
            }
            return result;
        }

        public async Task<bool> ClearOldBookings()
        {
            bool result = false;
            try
            {
                using var db = new ParkingContext();
                var oldBookings = db.Bookings.Where(b => b.Date <= DateTime.Now).ToList();
                if (oldBookings != null)
                {
                    foreach(var booking in oldBookings)
                    {
                        var spot = db.Spots.Where(s => s.SpotCode == booking.SpotCode).FirstOrDefault();
                        if (spot != null)
                        {
                            spot.BookingId = null;
                            spot.Booking = null;
                        }
                        booking.Status = "not actual";
                    }
                    await db.SaveChangesAsync();
                }
                result = true;

            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message);
                return false;
            }
            return result;
        }
    }
}
