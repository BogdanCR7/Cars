using Entities;

public interface IVehicleDataProvider
{
    Task<Vehicle?> GetByIdAsync(int id);
    Task<IEnumerable<Vehicle>> GetAllAsync();
    Task<IEnumerable<Vehicle>> GetByEmployeeIdAsync(int employeeId);
    Task AddAsync(Vehicle vehicle);
    Task UpdateAsync(Vehicle vehicle);
    Task DeleteAsync(int id);
    Task UpdateRangeAsync(IEnumerable<Vehicle> vehicles);
    Task<Vehicle?> GetByLicensePlateAsync(string licensePlate);
}
