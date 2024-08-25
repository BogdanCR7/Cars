using Cars.Common;

public class CreateEmployeeDto
{
    public string Name { get; set; }
    public string Position { get; set; }
    public ICollection<int> VehicleIds { get; set; } = new List<int>();
}