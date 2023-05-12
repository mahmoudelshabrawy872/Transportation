using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TransportationAPI.Models;
using TransportationAPI.Models.Dto.DriversDto;
using TransportationAPI.Repository.IRepository;

namespace TransportationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class DriverController : ControllerBase
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public DriverController(IDriverRepository driverRepository, IMapper mapper)
        {
            _driverRepository = driverRepository;
            this._response = new APIResponse();
            _mapper = mapper;
        }


        [HttpGet("GetAllDrivers")]
        public async Task<ActionResult<APIResponse>> GetAllDrivers()
        {
            try
            {
                IEnumerable<Driver> Drivers = await _driverRepository.GetAllAsync();
                _response.Result = _mapper.Map<List<DriverDto>>(Drivers);
                return _response;
            }
            catch (Exception e)
            {
                _response.Result = null;
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages = new()
                {
                    e.ToString()
                };
            }

            return _response;


        }

        [HttpGet("GetDriverById")]
        public async Task<ActionResult<APIResponse>> GetDriverById(int id)
        {
            try
            {
                var Driver = await _driverRepository.GetAsync(c => c.Id == id);
                if (Driver is null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessages = new()
                    {
                        "This Driver Not Found"
                    };
                }
                else
                {
                    _response.Result = _mapper.Map<DriverDto>(Driver);
                }
            }
            catch (Exception e)
            {
                _response.Result = null;
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages = new()
                {
                    e.ToString()
                };
            }
            return _response;
        }

        [HttpPost("CreateDriver")]
        public async Task<ActionResult<APIResponse>> CreateDriver([FromQuery] DriverDto dto)
        {
            Driver Driver = _mapper.Map<Driver>(dto);
            try
            {
                await _driverRepository.CreateAsync(Driver);

                _response.Result = Driver;

            }
            catch (Exception e)
            {
                _response.Result = null;
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages = new()
                {
                    e.ToString()
                };
            }
            return _response;

        }

        [HttpDelete("DeleteDriverById")]
        public async Task<ActionResult<APIResponse>> DeleteDriverById(int id)
        {


            var Driver = await _driverRepository.GetAsync(c => c.Id == id);
            if (Driver is null)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages = new()
                {
                    "This Id Is Not Find"
                };
                return _response;
            }
            try
            {

                await _driverRepository.DeleteAsync(Driver);
                bool IsDeleted = true;
                _response.Result = IsDeleted;

            }
            catch (Exception e)
            {
                _response.Result = null;
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages = new()
                {
                    e.ToString()
                };
            }
            return _response;


        }

        [HttpPut("UpdateDriverById")]
        public async Task<ActionResult<APIResponse>> UpdateDriverById([FromBody] DriverUpdateDto dto)
        {

            var Driver = await _driverRepository.GetAsync(c => c.Id == dto.Id, false);
            if (Driver is null)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages = new()
                {
                    "This Driver Is Not Find"
                };
                return _response;
            }

            try
            {
                Driver resultDriver = _mapper.Map<Driver>(dto);
                await _driverRepository.UpdateDriverAsync(resultDriver);
                _response.Result = resultDriver;

            }
            catch (Exception e)
            {
                _response.Result = null;
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages = new()
                {
                    e.ToString()
                };
            }
            return _response;
        }

    }
}
