using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkingApp.Model.EntityFramework;
using ParkingApp.Services.Employees;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ParkingApp.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ILogger<EmployeesController> _logger;
        private readonly IEmployeesService _employ;

        public EmployeesController(ILogger<EmployeesController> logger, IEmployeesService employ)
        {
            _logger = logger;
            _employ = employ;
        }
        [HttpGet]
        [Route("booking-balance")]
        public async Task<IActionResult> GetBalance()
        {
            var login = await _employ.CheckLogin(Request);
            if (login != null && login != string.Empty)
            {
                var res = await _employ.GetBalance(login);
                if (res is not null)
                    return Ok(new {balance = res});
                else
                    return BadRequest();
            }
            else
                return new StatusCodeResult(StatusCodes.Status203NonAuthoritative);
        }
        [HttpGet]
        [Route("available-spots")]
        public IActionResult GetFreeSpots() 
        {
            return Ok("GET available-spots");
        }
        [HttpPost]
        [Route("book-spot")]
        public IActionResult BookSpot()
        {
            return Ok("POST book-spot");
        }
        [HttpPost]
        [Route("bookings/{id}/cancel")]
        public IActionResult BookCancel(string id)
        {
            return Ok($"POST bookings/{id}/cancel");
        }
        [HttpGet]
        [Route("bookings")]
        public IActionResult GetBookings() 
        {
            return Ok("GET bookings");
        }
    }
}
