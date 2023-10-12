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
    }
}
