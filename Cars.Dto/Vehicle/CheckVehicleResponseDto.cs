using Cars.Common;

namespace Cars.Dto
{
    public class CheckVehicleResponseDto 
    {
        public int? EmployeeId { get; set; }
        public bool IsAuthorized { get; set; }
    }
}
