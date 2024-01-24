using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace ResturantReservation.Shared.Domain
{
    public class Reservation : BaseDomainModel
    {
        public DateTime DateOut { get; set; }
        public DateTime DateIn { get; set; }
        public int? RestaurantId { get; set; }
        public virtual Restaurant? Restaurant { get; set; }
        public int? CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }
        public int? TimeId {  get; set; }
        public virtual Time? Time { get; set; }
        public int GuestCount {  get; set; }
    }
}
