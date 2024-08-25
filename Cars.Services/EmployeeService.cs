using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Entities;

namespace Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeDataProvider _employeeDataProvider;
        private readonly IMapper _mapper;

        public IVehicleDataProvider _vehicleDataProvider { get; }

        public EmployeeService(IEmployeeDataProvider employeeDataProvider, IVehicleDataProvider vehicleDataProvider, IMapper mapper)
        {
            _employeeDataProvider = employeeDataProvider;
            _vehicleDataProvider = vehicleDataProvider;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
        {
            var employees = await _employeeDataProvider.GetAllAsync();
            return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }

        public async Task<EmployeeDto?> GetEmployeeByIdAsync(int id)
        {
            var employee = await _employeeDataProvider.GetByIdAsync(id);
            return employee != null ? _mapper.Map<EmployeeDto>(employee) : null;
        }

        public async Task<EmployeeDto?> CreateEmployeeAsync(CreateEmployeeDto createEmployeeDto)
        {
            var employee = _mapper.Map<Employee>(createEmployeeDto);

            await _employeeDataProvider.AddAsync(employee);

            var addedEmployee = await _employeeDataProvider.GetByIdAsync(employee.Id);
            await AttachVehicle(createEmployeeDto.VehicleIds, addedEmployee);

            var missingVehicles = await AttachVehicle(createEmployeeDto.VehicleIds, addedEmployee);

            if (missingVehicles.Any())
            {
                throw new Exception($"The following vehicles were not found: {string.Join(", ", missingVehicles)}");
            }

            return _mapper.Map<EmployeeDto>(addedEmployee);
        }

        public async Task<EmployeeDto?> UpdateEmployeeAsync(UpdateEmployeeDto updatedEmployeeDto)
        {
            var existingEmployee = await _employeeDataProvider.GetByIdAsync(updatedEmployeeDto.Id);
            if (existingEmployee == null)
            {
                throw new Exception("Employee doesn't exist");
            }

            _mapper.Map(updatedEmployeeDto, existingEmployee);
            await _employeeDataProvider.UpdateAsync(existingEmployee);
            var updatedEmployee = await _employeeDataProvider.GetByIdAsync(existingEmployee.Id);

            var missingVehicles = await AttachVehicle(updatedEmployeeDto.VehicleIds, existingEmployee);

            if (missingVehicles.Any())
            {
                throw new Exception($"The following vehicles were not found: {string.Join(", ", missingVehicles)}");
            }

            return _mapper.Map<EmployeeDto>(existingEmployee);
        }
        private async Task<List<int>> AttachVehicle(ICollection<int> vehicleIds, Employee? addedEmployee)
        {
            var missingVehicles = new List<int>();

            foreach (var vehicleId in vehicleIds)
            {
                var vehicle = await _vehicleDataProvider.GetByIdAsync(vehicleId);
                if (vehicle == null)
                {
                    missingVehicles.Add(vehicleId);
                    continue;
                }

                vehicle.EmployeeId = addedEmployee.Id;
                await _vehicleDataProvider.UpdateAsync(vehicle);
            }

            return missingVehicles;
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var existingEmployee = await _employeeDataProvider.GetByIdAsync(id);
            if (existingEmployee == null)
            {
                return false;
            }

            await _employeeDataProvider.DeleteAsync(id);
            return true;
        }
    }
}
