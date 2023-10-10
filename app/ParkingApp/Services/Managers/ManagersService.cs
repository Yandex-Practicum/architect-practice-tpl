using ParkingApp.Model.EntityFramework;

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
            using var db = new ParkingContext();
            var res = (from emp in db.Employees
                      select emp.Login).ToList();
            if (res.Count > 0)
                result = res;
            return result;
        }
    
    }
}
