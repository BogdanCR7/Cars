using Cars.Common;

public class UpdateEmployeeDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Position { get; set; }
    public ICollection<int> VehicleIds { get; set; } = new List<int>();
}