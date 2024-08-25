using Microsoft.AspNetCore.Mvc;
using Services;
using Cars.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Entities;
using Cars.Api.Extensions;
using Cars.Dto;

namespace Cars.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;
        private readonly IMapper _mapper;

        public VehiclesController(IVehicleService vehicleService, IMapper mapper)
        {
            _vehicleService = vehicleService;
            _mapper = mapper;
        }

        [HttpGet("check/{licensePlate}/employee/{employeeId}")]
        public async Task<ActionResult<ServerResponse<CheckVehicleResponseDto>>> CheckVehicle(string licensePlate, int employeeId)
        {
            return await this.HandleException(async () =>
            {
                var result = await _vehicleService.CheckVehicleAsync(licensePlate, employeeId);

                return result;
            });
        }

        [HttpGet]
        public async Task<ActionResult<ServerResponse<IEnumerable<VehicleDto>>>> GetAllVehicles()
        {
            return await this.HandleException(async () =>
            {
                var vehicles = await _vehicleService.GetAllVehiclesAsync();
                var vehicleDtos = _mapper.Map<IEnumerable<VehicleDto>>(vehicles);
                return vehicleDtos;
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServerResponse<VehicleDto>>> GetVehicleById(int id)
        {
            return await this.HandleException(async () =>
            {
                var vehicle = await _vehicleService.GetVehicleByIdAsync(id);
                if (vehicle == null)
                {
                    throw new Exception("Vehicle not found");
                }

                var vehicleDto = _mapper.Map<VehicleDto>(vehicle);
                return vehicleDto;
            });
        }

        [HttpPost]
        public async Task<ActionResult<ServerResponse<VehicleDto>>> CreateVehicle([FromBody] CreateVehicleDto createVehicleDto)
        {
            return await this.HandleException(async () =>
            {
                var createdVehicle = await _vehicleService.CreateVehicleAsync(createVehicleDto);
                var vehicleDto = _mapper.Map<VehicleDto>(createdVehicle);

                return vehicleDto;
            });
        }

        [HttpPut]
        public async Task<ActionResult<ServerResponse<VehicleDto>>> UpdateVehicle([FromBody] UpdateVehicleDto updateVehicleDto)
        {
            return await this.HandleException(async () =>
            {
                var updatedVehicle = await _vehicleService.UpdateVehicleAsync(updateVehicleDto);
                var vehicleDto = _mapper.Map<VehicleDto>(updatedVehicle);

                return vehicleDto;
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServerResponse<SuccessResponseDto>>> DeleteVehicle(int id)
        {
            return await this.HandleException(async () =>
            {
                await _vehicleService.DeleteVehicleAsync(id);
                return new SuccessResponseDto();
            });
        }
    }
}
