using Entities;

public interface IEmployeeService
{
    Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync();
    Task<EmployeeDto?> GetEmployeeByIdAsync(int id);
    Task<EmployeeDto?> CreateEmployeeAsync(CreateEmployeeDto employeeDto);
    Task<EmployeeDto?> UpdateEmployeeAsync(UpdateEmployeeDto updatedEmployeeDto);
    Task<bool> DeleteEmployeeAsync(int id);
}
