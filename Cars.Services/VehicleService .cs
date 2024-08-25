using AutoMapper;
using Cars.Dto;
using Entities;

public class VehicleService : IVehicleService
{
    private readonly IVehicleDataProvider _vehicleDataProvider;
    private readonly IMapper _mapper;

    public IEmployeeDataProvider _employeeDataProvider { get; }

    public VehicleService(IVehicleDataProvider vehicleDataProvider, IEmployeeDataProvider employeeDataProvider,IMapper mapper)
    {
        _vehicleDataProvider = vehicleDataProvider;
        _employeeDataProvider = employeeDataProvider;
        _mapper = mapper;
    }

    public async Task<CheckVehicleResponseDto> CheckVehicleAsync(string licensePlate, int employeeId)
    {
        var vehicle = await _vehicleDataProvider.GetByLicensePlateAsync(licensePlate);

        if (vehicle == null)
        {
            return new CheckVehicleResponseDto
            {
                IsAuthorized = false,
                EmployeeId = employeeId
            };
        }

        if (vehicle.EmployeeId != employeeId)
        {
            return new CheckVehicleResponseDto
            {
                IsAuthorized = false,
                EmployeeId = employeeId
            };
        }

        var employee = await _employeeDataProvider.GetByIdAsync(employeeId);

        if (employee == null)
        {
            return new CheckVehicleResponseDto
            {
                IsAuthorized = false,
                EmployeeId = employeeId
            };
        }

        return new CheckVehicleResponseDto
        {
            IsAuthorized = true,
            EmployeeId = employee.Id,
        };
    }

    public async Task<VehicleDto> GetVehicleByIdAsync(int id)
    {
        var vehicle = await _vehicleDataProvider.GetByIdAsync(id);
        return _mapper.Map<VehicleDto>(vehicle);
    }

    public async Task<IEnumerable<VehicleDto>> GetAllVehiclesAsync()
    {
        var vehicles = await _vehicleDataProvider.GetAllAsync();
        return _mapper.Map<IEnumerable<VehicleDto>>(vehicles);
    }

    public async Task<IEnumerable<VehicleDto>> GetVehiclesByEmployeeIdAsync(int employeeId)
    {
        var vehicles = await _vehicleDataProvider.GetByEmployeeIdAsync(employeeId);
        return _mapper.Map<IEnumerable<VehicleDto>>(vehicles);
    }

    public async Task<Vehicle> CreateVehicleAsync(CreateVehicleDto vehicleDto)
    {
        var employee = await _employeeDataProvider.GetByIdAsync(vehicleDto.EmployeeId);

        if (employee == null)
        {
            throw new Exception("Employee not found");
        }
        var vehicle = _mapper.Map<Vehicle>(vehicleDto);
        await _vehicleDataProvider.AddAsync(vehicle);

        return vehicle;
    }

    public async Task<Vehicle> UpdateVehicleAsync(UpdateVehicleDto vehicleDto)
    {
        var existingVehicle = await _vehicleDataProvider.GetByIdAsync(vehicleDto.Id);
        if (existingVehicle == null)
        {
            throw new Exception("Vehicle not found");
        }

        var employee = await _employeeDataProvider.GetByIdAsync(vehicleDto.EmployeeId);

        if (employee == null)
        {
            throw new Exception("Employee not found");
        }

        _mapper.Map(vehicleDto, existingVehicle);
        await _vehicleDataProvider.UpdateAsync(existingVehicle);

        return existingVehicle;
    }

    public async Task DeleteVehicleAsync(int id)
    {
        await _vehicleDataProvider.DeleteAsync(id);
    }
}
