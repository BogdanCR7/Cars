using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class Vehicle
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "License plate is required")]
        [StringLength(10, ErrorMessage = "License plate can't be longer than 10 characters")]
        public string LicensePlate { get; set; }

        [Required(ErrorMessage = "Employee ID is required")]
        public int EmployeeId { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public Employee Employee { get; set; }
    }
}
