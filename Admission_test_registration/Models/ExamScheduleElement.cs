using System.ComponentModel.DataAnnotations;

namespace Admission_test_registration.Models
{
    public class ExamScheduleElement
    {
        public int ExamScheduleElementId { get; set; }
        [Required]
        public University? University { get; set; }
        public int UniversityId { get; set; }

        [Required]
        public DateTime ExamTime { get; set; }

        public List<RegistrationSheet> RegistrationSheets { get; set; }
        

        public ExamScheduleElement(){
            RegistrationSheets = new List<RegistrationSheet>();
        }
    }
}
