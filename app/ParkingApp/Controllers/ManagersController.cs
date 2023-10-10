using Microsoft.AspNetCore.Mvc;
using ParkingApp.Model;
using ParkingApp.Services.Managers;
using System.Text.Json;

namespace ParkingApp.Controllers
{
    [ApiController]
    [Route("api/managers")]
    public class ManagersController : ControllerBase
    {
        private readonly ILogger<ManagersController> _logger;
        private readonly IManagersService _managers;
        private string _managerCode = string.Empty;

        public ManagersController(ILogger<ManagersController> logger, IManagersService managers)
        {
            _logger = logger;
            _managers = managers;
        }
        /// <summary>
        /// Регистрация менеджера
        /// </summary>
        /// <param name="reg"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(ManagerRegisterQuery reg)
        {
            if (!Request.Headers.TryGetValue("X-Manager-Login", out var login) || login.Count == 0)
                return BadRequest();
            if (await _managers.Register(reg.SecretCode, login))
                return Ok();
            else
                return BadRequest();
        }
        /// <summary>
        /// Получение списка сотрудников
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("employees")]
        public async Task<IActionResult> GetEmployees() 
        {
            var res = await _managers.GetEmployees();
            return Ok(new { employees = res });
        }

        [HttpGet]
        [Route("employees/{login}")]
        public IActionResult GetEmployee(string login)
        {
            return Ok($"GET employee {login}");
        }
        [HttpPost]
        [Route("employees")]
        public IActionResult AddEmployee()
        {
            return Ok("POST employees");
        }

        [HttpDelete]
        [Route("employees/{login}")]
        public IActionResult DeleteEmployee(string login)
        {
            return Ok($"DELETE employees {login}");
        }

        [HttpPost]
        [Route("parking-scheme")]
        public IActionResult LoadParkingScheme()
        {
            return Ok("POST parking-scheme");
        }
        [HttpPost]
        [Route("parking-spots")]
        public IActionResult AddSpot()
        {
            return Ok("POST parking-spots");
        }
        [HttpGet]
        [Route("parking-spots")]
        public IActionResult GetSpots()
        {
            return Ok("GET parking-spots");
        }
        [HttpDelete]
        [Route("parking-spots/{spotCode}")]
        public IActionResult DeleteSpot(string spotCode)
        {
            return Ok($"DELETE parking-spots/{spotCode}");
        }

        [HttpPost]
        [Route("booking-limits")]
        public IActionResult SetLimits()
        {
            return Ok("POST booking-limits");
        }
        [HttpPost]
        [Route("booking-limits/{employeeLogin}")]
        public IActionResult SetPersonalLimits(string employeeLogin)
        {
            return Ok($"POST booking-limits/{employeeLogin}");
        }

        [HttpPost]
        [Route("email-settings")]
        public IActionResult SetEmailSettings() 
        {
            return Ok("POST email-settings");
        }
    }
}