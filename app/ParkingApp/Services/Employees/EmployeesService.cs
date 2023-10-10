namespace ParkingApp.Services.Employees
{
    public class EmployeesService: IEmployeesService
    {
        private readonly ILogger<EmployeesService> _logger;

        public EmployeesService(ILogger<EmployeesService> logger) 
        {
            _logger = logger;
        }
    }
}
