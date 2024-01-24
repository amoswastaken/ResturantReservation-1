using System.ComponentModel.DataAnnotations;

namespace ResturantReservation.Shared.Domain
{
    public class Customer : BaseDomainModel
    {
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "First Name does not meet the length requirements")]
        public string? FirstName { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Last Name does not meet the length requirements")]
        public string? LastName { get; set; }
        public string? Address { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"(6|8|9)\d{7}", ErrorMessage = "Contect Number is not a valid phone number")]
        public string? ContactNumber { get; set; }
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email Address is not a valid email")]
        [EmailAddress]
        public string? EmailAddress { get; set; }
        public virtual List<Reservation>? Reservations { get; set; }
    }
}
