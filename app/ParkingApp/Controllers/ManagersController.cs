using Microsoft.AspNetCore.Mvc;
using ParkingApp.Model;
using ParkingApp.Model.EntityFramework;
using ParkingApp.Services.Managers;
using System.Text.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
            if (await _managers.CheckManagerRequest(Request))
            {
                var res = await _managers.GetEmployees();
                return Ok(new { employees = res });
            }
            else
                return new StatusCodeResult(StatusCodes.Status203NonAuthoritative);
        }
        /// <summary>
        /// Получение информации о сотруднике
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("employees/{login}")]
        public async Task<IActionResult> GetEmployee(string login)
        {
            if (await _managers.CheckManagerRequest(Request))
            {
                var res = _managers.GetEmployee(login);
                if (res is not null)
                    return Ok(res);
                else
                    return NotFound();
            }
            else
                return new StatusCodeResult(StatusCodes.Status203NonAuthoritative);
        }
        [HttpPost]
        [Route("employees")]
        public async Task<IActionResult> AddEmployee(AddEmployeeQuery query)
        {
            if (await _managers.CheckManagerRequest(Request))
            {
                var res = await _managers.AddEmployee(query.Login);
                if (res)
                    return Ok();
                else
                    return BadRequest();
            }
            else
                return new StatusCodeResult(StatusCodes.Status203NonAuthoritative);
        }

        [HttpDelete]
        [Route("employees/{login}")]
        public async Task<IActionResult> DeleteEmployee(string login)
        {
            if (await _managers.CheckManagerRequest(Request))
            {
                var res = await _managers.DeleteEmployee(login);
                if (res)
                    return Ok();
                else
                    return BadRequest();
            }
            else
                return new StatusCodeResult(StatusCodes.Status203NonAuthoritative);
        }

        [HttpPost]
        [Route("parking-scheme")]
        public async Task<IActionResult> LoadParkingScheme()
        {
            if (await _managers.CheckManagerRequest(Request))
            {
                try
                {
                    var res = Request.BodyReader.AsStream();
                    FileStream fs = new(Path.Join(".", "ParkingScheme", "scheme.png"), FileMode.Create);
                    res.CopyTo(fs);
                    fs.Flush();
                    fs.Close();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
                return Ok();
            }
            else
                return new StatusCodeResult(StatusCodes.Status203NonAuthoritative);
        }
        /// <summary>
        /// Добавление парковочных мест в систему
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("parking-spots")]
        public async Task<IActionResult> AddSpots(List<string> spots)
        {
            if (await _managers.CheckManagerRequest(Request))
            {
                try
                {
                    var res = await _managers.AddSpots(spots);
                    if (res)
                        return Ok();
                    else
                        return BadRequest();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
                return new StatusCodeResult(StatusCodes.Status203NonAuthoritative);
        }
        /// <summary>
        /// Получение всех доступных парковочных мест
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("parking-spots")]
        public async Task<IActionResult> GetSpots()
        {
            if (await _managers.CheckManagerRequest(Request))
            {
                try
                {
                    var res = await _managers.GetAllSpots();
                    if (res != null)
                        return Ok(new { parking_spots = res});
                    else
                        return BadRequest();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
                return new StatusCodeResult(StatusCodes.Status203NonAuthoritative);
        }
        /// <summary>
        /// Удаление парковочного места
        /// </summary>
        /// <param name="spotCode"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("parking-spots/{spotCode}")]
        public async Task<IActionResult> DeleteSpot(string spotCode)
        {
            if (await _managers.CheckManagerRequest(Request))
            {
                var res = await _managers.DeleteSpot(spotCode);
                if (res)
                    return Ok();
                else
                    return BadRequest();
            }
            else
                return new StatusCodeResult(StatusCodes.Status203NonAuthoritative);
        }
        /// <summary>
        /// Установка лимитов для всех сотрудников
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("booking-limits")]
        public async  Task<IActionResult> SetLimits(AddLimitQuery query)
        {
            if (await _managers.CheckManagerRequest(Request))
            {
                var res = await _managers.SetGeneralLimit(query.Limit);
                if (res)
                    return Ok();
                else
                    return BadRequest();
            }
            else
                return new StatusCodeResult(StatusCodes.Status203NonAuthoritative);
        }
        [HttpPost]
        [Route("booking-limits/{employeeLogin}")]
        public async Task<IActionResult> SetPersonalLimits(string employeeLogin, [FromBody] AddLimitQuery query)
        {
            if (await _managers.CheckManagerRequest(Request))
            {
                var res = await _managers.SetPersonalLimit(employeeLogin ,query.Limit);
                if (res)
                    return Ok();
                else
                    return BadRequest();
            }
            else
                return new StatusCodeResult(StatusCodes.Status203NonAuthoritative);
        }

        [HttpPost]
        [Route("email-settings")]
        public async Task<IActionResult> SetEmailSettings(NotifyRuleQuery query) 
        {
            if (await _managers.CheckManagerRequest(Request))
            {
                var res = await _managers.SetNotifyRule(query);
                if (res)
                    return Ok();
                else
                    return BadRequest();
            }
            else
                return new StatusCodeResult(StatusCodes.Status203NonAuthoritative);
        }
    }
}