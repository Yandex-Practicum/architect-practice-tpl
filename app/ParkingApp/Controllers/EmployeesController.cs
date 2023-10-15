using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkingApp.Model;
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
        /// <summary>
        /// Получение текущего баланса бронирования
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Получение доступных мест бронирования
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("available-spots")]
        public async Task<IActionResult> GetFreeSpots() 
        {
            var login = await _employ.CheckLogin(Request);
            if (login != null && login != string.Empty)
            {
                var res = await _employ.GetAvailableSpots();
                if (res is not null)
                    return Ok(new { date = $"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}",
                                    available_spots = res});
                else
                    return BadRequest();
            }
            else
                return new StatusCodeResult(StatusCodes.Status203NonAuthoritative);
        }
        [HttpPost]
        [Route("book-spot")]
        public async Task<IActionResult> BookSpot(BookSpotQuery query)
        {
            var login = await _employ.CheckLogin(Request);
            if (login != null && login != string.Empty)
            {
                var res = await _employ.BookSpot(login, query);
                if (res)
                    return Ok(); 
                else
                    return BadRequest();
            }
            else
                return new StatusCodeResult(StatusCodes.Status203NonAuthoritative);
        }
        [HttpPost]
        [Route("bookings/{id}/cancel")]
        public async Task<IActionResult> BookCancel(string id)
        {
            var login = await _employ.CheckLogin(Request);
            if (login != null && login != string.Empty)
            {
                var res = await _employ.CancelBooking(id);
                if (res)
                    return Ok();
                else
                    return BadRequest();
            }
            else
                return new StatusCodeResult(StatusCodes.Status203NonAuthoritative);
        }
        [HttpGet]
        [Route("bookings")]
        public async Task<IActionResult> GetBookings() 
        {
            var login = await _employ.CheckLogin(Request);
            if (login != null && login != string.Empty)
            {
                var res = await _employ.GetBookings(login);
                if (res is not null)
                    return Ok(new { bookings = res});
                else
                    return BadRequest();
            }
            else
                return new StatusCodeResult(StatusCodes.Status203NonAuthoritative);
        }
    }
}
