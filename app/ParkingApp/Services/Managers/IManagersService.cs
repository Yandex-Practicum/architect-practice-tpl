using ParkingApp.Model.EntityFramework;

namespace ParkingApp.Services.Managers
{
    public interface IManagersService
    {
        Task<bool> AddEmployee(string login);
        Task<bool> AddSpots(List<string> spots);
        Task<bool> CheckManagerRequest(HttpRequest request);
        Task<bool> DeleteEmployee(string login);
        Task<bool> DeleteSpot(string spot);
        Task<List<string>> GetAllSpots();
        Task<Employee?> GetEmployee(string login);
        Task<List<string>> GetEmployees();
        Task<bool> Register(string code, string login);
    }
}
