using Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>().HasData(
            new Employee { Id = 1, Name = "John Doe", Position = "Security Guard" },
            new Employee { Id = 2, Name = "Jane Smith", Position = "Manager" }
        );

        modelBuilder.Entity<Vehicle>().HasData(
            new Vehicle { Id = 1, LicensePlate = "ABC123", EmployeeId = 1 },
            new Vehicle { Id = 2, LicensePlate = "AHD789", EmployeeId = 2 },
            new Vehicle { Id = 3, LicensePlate = "LMN456", EmployeeId = 1 }
        );

    }
}
