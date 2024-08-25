using Context;
using Entities;
using Microsoft.EntityFrameworkCore;

public class VehicleDataProvider : IVehicleDataProvider
{
    private readonly AppDbContext _context;

    public VehicleDataProvider(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Vehicle?> GetByIdAsync(int id)
    {
        return await _context.Vehicles.FindAsync(id);
    }

    public async Task<IEnumerable<Vehicle>> GetAllAsync()
    {
        return await _context.Vehicles.ToListAsync();
    }

    public async Task<IEnumerable<Vehicle>> GetByEmployeeIdAsync(int employeeId)
    {
        return await _context.Vehicles
            .Where(v => v.EmployeeId == employeeId)
            .ToListAsync();
    }

    public async Task AddAsync(Vehicle vehicle)
    {
        _context.Vehicles.Add(vehicle);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Vehicle vehicle)
    {
        _context.Vehicles.Update(vehicle);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var vehicle = await GetByIdAsync(id);
        if (vehicle != null)
        {
            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateRangeAsync(IEnumerable<Vehicle> vehicles)
    {
        _context.Vehicles.UpdateRange(vehicles);
        await _context.SaveChangesAsync();
    }

    public async Task<Vehicle?> GetByLicensePlateAsync(string licensePlate)
    {
        return await _context.Vehicles.SingleOrDefaultAsync(x => x.LicensePlate==licensePlate);

    }
}
