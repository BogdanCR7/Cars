using Cars.Dto;
using Entities;

public interface IVehicleService
{
    Task<VehicleDto> GetVehicleByIdAsync(int id);
    Task<IEnumerable<VehicleDto>> GetAllVehiclesAsync();
    Task<IEnumerable<VehicleDto>> GetVehiclesByEmployeeIdAsync(int employeeId);
    Task<Vehicle> CreateVehicleAsync(CreateVehicleDto vehicleDto);
    Task<Vehicle> UpdateVehicleAsync(UpdateVehicleDto vehicleDto);
    Task DeleteVehicleAsync(int id);
    Task<CheckVehicleResponseDto> CheckVehicleAsync(string licensePlate, int employeeId);

}
