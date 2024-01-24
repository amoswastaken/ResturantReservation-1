using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantReservation.Shared.Domain
{
    public class Diet : BaseDomainModel
    {
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Diet type does not meet the length requirements")]
        public string? Name { get; set; }
    }
}
