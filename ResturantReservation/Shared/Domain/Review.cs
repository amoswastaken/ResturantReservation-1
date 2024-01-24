using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantReservation.Shared.Domain
{
    public class Review : BaseDomainModel
    {
        public int CustomerId {  get; set; }
        public virtual Customer? Customer { get; set; }
        [Required(ErrorMessage = "Score is required")]
        [Range(0, 5, ErrorMessage = "Score must be between 0 and 5")]
        public int Score { get; set; }
        public string? Content {  get; set; }
        public int RestaurantId { get; set; }
        public virtual Restaurant? Restaurant { get; set; }
    }
}
