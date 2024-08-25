using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Employee
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name length can't be more than 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Position is required")] 
        [StringLength(50,MinimumLength =1, ErrorMessage = "Position length can't be more than 50 characters")]
        public string Position { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
