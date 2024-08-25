using Cars.Common;

public class EmployeeDto 
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Position { get; set; }
    public ICollection<VehicleDto> Vehicles { get; set; } = new List<VehicleDto>();
}
