using System.ComponentModel.DataAnnotations;
//using Admission_test_registration.CustomTags;

namespace Admission_test_registration.Models
{
    public class Educator : PersonInterface
    {
        public int EducatorId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "The text length must be from 2 to 100 characters")]
        public string Name { get; set; } = "";
        [Required(ErrorMessage = "Tel. number is required")]
        [RegularExpression(@"^\+380\d{3}\d{2}\d{2}\d{2}$", ErrorMessage = "Incorect form of tel. number")]
        public string telNumber { get; set; } = string.Empty;
        [Required(ErrorMessage = "E-mail is required")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Incorect form of e-mail")]
        public string Email { get; set; } = "";
        [Required(ErrorMessage = "Adress is required")]
        [StringLength(100, MinimumLength = 7, ErrorMessage = "The text length must be from 7 to 100 characters")]
        public string Adress { get; set; } = "";

        [Required]
        //[AgeValidationTag(20, ErrorMessage = "Person should be at least 20 years old.")]
        [DateTimeValidation(20, 65)]
        public DateOnly DateOfBirth { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public Sex Sex { get; set; }
        [Required]
        [Range(1, 6)]
        public int Degree { get; set; }

        public List<Certificate> CertificateList { get; set; }
        public Educator() {
            CertificateList = new List<Certificate>();
        }
    }
}
