using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ParkingApp.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ILogger<EmployeesController> _logger;

        public EmployeesController(ILogger<EmployeesController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        [Route("booking-balance")]
        public IActionResult GetBalance()
        {
            return Ok("GET booking-balance");
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
