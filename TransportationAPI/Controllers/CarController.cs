﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TransportationAPI.Models;
using TransportationAPI.Models.Dto.CarsDto;
using TransportationAPI.Repository.IRepository;

namespace TransportationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CarController : ControllerBase
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public CarController(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            this._response = new APIResponse();
            _mapper = mapper;
        }


        [HttpGet("GetAllCars")]
        public async Task<ActionResult<APIResponse>> GetAllCars()
        {
            try
            {
                IEnumerable<Car> cars = await _carRepository.GetAllAsync();
                _response.Result = _mapper.Map<List<CarDto>>(cars);
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

        [HttpGet("GetCarById")]
        public async Task<ActionResult<APIResponse>> GetCarById(int id)
        {
            try
            {
                var car = await _carRepository.GetAsync(c => c.Id == id);
                if (car is null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessages = new()
                    {
                        "This Car Not Found"
                    };
                }
                else
                {
                    _response.Result = _mapper.Map<CarDto>(car);
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

        [HttpGet("SearchOnCars")]
        public async Task<ActionResult<APIResponse>> SearchOnCars(string searchKey)
        {
            try
            {

                var car = await _carRepository.GetAllAsync(c =>
                    c.Model.ToLower().Contains(searchKey.ToLower()) ||
                    c.FramNumber.ToLower().Contains(searchKey.ToLower()) ||
                    c.Color.ToLower().Contains(searchKey.ToLower()) ||
                    c.Name.ToLower().Contains(searchKey.ToLower()) ||
                    c.MoterNumber.ToLower().Contains(searchKey.ToLower()) ||
                    c.PlateNumber.ToLower().Contains(searchKey.ToLower()) ||
                    c.OwnerName.ToLower().Contains(searchKey.ToLower()) ||
                    c.Kind.ToLower().Contains(searchKey.ToLower())
                );
                if (car is null || !car.Any())
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessages = new()
                    {
                        "This search Key Not Found"
                    };
                    return _response;
                }
                else
                {
                    _response.Result = _mapper.Map<List<CarDto>>(car);
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


        [HttpPost("CreateCar")]
        public async Task<ActionResult<APIResponse>> CreateCar([FromQuery] CarDto dto)
        {
            Car car = _mapper.Map<Car>(dto);
            try
            {
                await _carRepository.CreateAsync(car);

                _response.Result = car;

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

        [HttpDelete("DeleteCarById")]
        public async Task<ActionResult<APIResponse>> DeleteCarById(int id)
        {


            var car = await _carRepository.GetAsync(c => c.Id == id);
            if (car is null)
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

                await _carRepository.DeleteAsync(car);
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

        [HttpPut("UpdateCarById")]
        public async Task<ActionResult<APIResponse>> UpdateCarById([FromBody] CarUpdateDto dto)
        {

            var car = await _carRepository.GetAsync(c => c.Id == dto.Id, false);
            if (car is null)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages = new()
                {
                    "This Car Is Not Find"
                };
                return _response;
            }

            try
            {
                Car resultCar = _mapper.Map<Car>(dto);
                await _carRepository.UpdateCarAsync(resultCar);
                _response.Result = resultCar;

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
