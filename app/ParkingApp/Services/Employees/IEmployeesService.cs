namespace ParkingApp.Services.Employees
{
    public interface IEmployeesService
    {
        Task<string?> CheckLogin(HttpRequest request);
        Task<int?> GetBalance(string login);
    }
}
