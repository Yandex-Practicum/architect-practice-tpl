using Microsoft.EntityFrameworkCore;
using ParkingApp.Model.EntityFramework;
using System.ComponentModel;

namespace ParkingApp.Services.Managers
{
    public class ManagersService: IManagersService
    {
        private readonly ILogger<ManagersService> _logger;

        public ManagersService(ILogger<ManagersService> logger)
        {
            _logger = logger;
        }
        public async Task<bool> Register(string code, string login)
        {
            bool result = false;
            var codeEnv = Environment.GetEnvironmentVariable("SECRET_CODE");
            if (codeEnv != code)
                return false;
            using var db = new ParkingContext();
            try
            {
                var manager = db.Managers.Where(m => m.Login == login).FirstOrDefault();
                if (manager != null)
                {
                    result = true;
                }
                else
                {
                    db.Managers.Add(new Manager { SecretCode = code, Login = login });
                    var res = await db.SaveChangesAsync();
                    if (res > 0)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
            return result;
        }
    
        public async Task<List<string>> GetEmployees()
        {
            List<string> result = new();
            try
            {
                using var db = new ParkingContext();
                var res = (from emp in db.Employees
                           select emp.Login).ToList();
                if (res.Count > 0)
                    result = res;
            }
            catch(Exception ex)
            {
                _logger?.LogError(ex.Message);
                return new List<string>();
            }
            return result;
        }
        public async Task<Employee?> GetEmployee(string login)
        {
            Employee? result = null;
            try
            {
                using var db = new ParkingContext();
                var res = db.Employees.Where(e => e.Login == login)
                                      .Include(e => e.Bookings).FirstOrDefault();
                result = res;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
            return result;
        }
        public async Task<bool> AddEmployee(string login)
        {
            bool result = false;
            try
            {
                using var db = new ParkingContext();
                db.Employees.Add(new Employee() { Login = login });
                var res = await db.SaveChangesAsync();
                if (res > 0)
                    result = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
            return result;
        }
        public async Task<bool> DeleteEmployee(string login)
        {
            bool result = false;
            try
            {
                using var db = new ParkingContext();
                var delEmp = db.Employees.Where(e => e.Login == login).FirstOrDefault();
                if (delEmp is not null)
                {
                    db.Employees.Remove(delEmp);
                    var res = await db.SaveChangesAsync();
                    if (res > 0)
                        result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
            return result;
        }

        public async Task<bool> AddSpots(List<string> spots)
        {
            bool result = false;
            try
            {
                using var db = new ParkingContext();
                foreach (string s in spots)
                {
                    var exist = db.Spots.Any(sp => sp.SpotCode == s);
                    if (!exist)
                        db.Spots.Add(new Spot { SpotCode = s });
                }
                var res = await db.SaveChangesAsync();
                result = res > 0;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
            return result;
        }

        public async Task<List<string>> GetAllSpots()
        {
            List<string> result = new ();
            try
            {
                using var db = new ParkingContext();
                var res = (from sp in db.Spots
                           select sp.SpotCode).ToList();
                if (res is not null)
                    result = res;  
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new List<string>();
            }
            return result;
        }

        public async Task<bool> DeleteSpot(string spot)
        {
            bool result = false;
            try
            {
                using var db = new ParkingContext();
                var delSpot = db.Spots.Where(s => s.SpotCode == spot).FirstOrDefault();
                if (delSpot is not null)
                {
                    db.Spots.Remove(delSpot);
                    var res = await db.SaveChangesAsync();
                    if (res > 0)
                        result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
            return result;
        }

        public async Task<bool> CheckManagerRequest(HttpRequest request)
        {
            bool result = false;
            if (!request.Headers.TryGetValue("X-Manager-Login", out var login) || login.Count == 0)
                result = false;
            else
            {
                var rrr = login.First();
                using var db = new ParkingContext();
                var res = (from mgr in db.Managers
                          where mgr.Login == login.First()
                          select mgr).FirstOrDefault();
                result = res is not null;
            }
            return result;
        }
    }
}
