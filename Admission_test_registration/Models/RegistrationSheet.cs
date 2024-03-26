using System.ComponentModel.DataAnnotations;

namespace Admission_test_registration.Models
{
    public class RegistrationSheet
    {
        public int RegistrationSheetId { get; set; }

        public Student? Student { get; set; }
        
        public int StudentId { get; set; }
        public ExamScheduleElement? ExamScheduleElement { get; set; }
        
        public int ExamScheduleElementId { get; set; }

        public DateTime RegistrationTime { get; set; }

        public RegistrationSheet() {
        }
    }
}
