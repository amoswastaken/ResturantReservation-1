using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResturantReservation.Shared.Domain
{
    public class Restaurant : BaseDomainModel
    {
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Restaurant name does not meet the length requirements")]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? LocationId {  get; set; }
        public virtual Location? Location { get; set; }
        public string? Address { get; set; }
        public int? CuisineId {  get; set; }
        public virtual Cuisine? Cuisine { get; set; }
        public int? DietId { get; set; }
        public virtual Diet? Diet { get; set; }
    }
}
