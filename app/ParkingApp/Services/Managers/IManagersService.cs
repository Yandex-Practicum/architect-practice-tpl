namespace ParkingApp.Services.Managers
{
    public interface IManagersService
    {
        Task<List<string>> GetEmployees();
        Task<bool> Register(string code, string login);
    }
}
