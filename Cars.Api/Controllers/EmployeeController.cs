using Microsoft.AspNetCore.Mvc;
using Services;
using Cars.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Entities;
using Cars.Api.Extensions;

namespace Cars.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public EmployeesController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ServerResponse<IEnumerable<EmployeeDto>>>> GetAllEmployees()
        {
            return await this.HandleException(async () =>
            {
                var employees = await _employeeService.GetAllEmployeesAsync();
                var employeeDtos = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
                return employeeDtos;
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServerResponse<EmployeeDto>>> GetEmployeeById(int id)
        {
            return await this.HandleException(async () =>
            {
                var employee = await _employeeService.GetEmployeeByIdAsync(id);
                if (employee == null)
                {
                    throw new Exception("Employee not found");
                }

                var employeeDto = _mapper.Map<EmployeeDto>(employee);
                return employeeDto;
            });
        }

        [HttpPost]
        public async Task<ActionResult<ServerResponse<EmployeeDto>>> CreateEmployee([FromBody] CreateEmployeeDto createEmployeeDto)
        {
            return await this.HandleException(async () =>
            {
                var createdEmployee = await _employeeService.CreateEmployeeAsync(createEmployeeDto);
                var employeeDto = _mapper.Map<EmployeeDto>(createdEmployee);

                return employeeDto;
            });
        }

        [HttpPut]
        public async Task<ActionResult<ServerResponse<EmployeeDto>>> UpdateEmployee( [FromBody] UpdateEmployeeDto updateEmployeeDto)
        {
            return await this.HandleException(async () =>
            {
                var updatedEmployee = await _employeeService.UpdateEmployeeAsync(updateEmployeeDto);
                var employeeDto = _mapper.Map<EmployeeDto>(updatedEmployee);

                return employeeDto;
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServerResponse<SuccessResponseDto>>> DeleteEmployee(int id)
        {
            return await this.HandleException(async () =>
            {
                await _employeeService.DeleteEmployeeAsync(id);
                return new SuccessResponseDto();
            });

        }
    }
}
