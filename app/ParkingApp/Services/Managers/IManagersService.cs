using ParkingApp.Model.EntityFramework;

namespace ParkingApp.Services.Managers
{
    public interface IManagersService
    {
        Task<bool> AddEmployee(string login);
        Task<bool> CheckManagerRequest(HttpRequest request);
        Task<Employee?> GetEmployee(string login);
        Task<List<string>> GetEmployees();
        Task<bool> Register(string code, string login);
    }
}
