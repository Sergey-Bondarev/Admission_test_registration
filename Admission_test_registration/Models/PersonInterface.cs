using System.ComponentModel.DataAnnotations;

namespace Admission_test_registration.Models
{
    public enum Sex
    {
        Male,
        Female,
    }
    public interface PersonInterface
    {
        public string Name { get; set; }
        public string telNumber { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public Sex Sex { get; set; }
    }
}
