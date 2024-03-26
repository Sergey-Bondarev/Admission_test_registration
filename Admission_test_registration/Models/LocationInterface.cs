using System.ComponentModel.DataAnnotations;

namespace Admission_test_registration.Models
{
    public interface LocationInterface
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "The text length must be from 2 to 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Adress is required")]
        [StringLength(100, MinimumLength = 7, ErrorMessage = "The text length must be from 7 to 100 characters")]
        public string Adress { get; set; }

        [Required]
        [Range(-90.0, 90.0)]
        public double Latitude { get; set; }
        [Required]
        [Range(-180.0, 180.0)]
        public double Longitude { get; set; }

    }
}
