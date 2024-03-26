using System.ComponentModel.DataAnnotations;

namespace Admission_test_registration.Models
{
    public class University : LocationInterface
    {
        public int UniversityId {  get; set; }
 
        public string Name { get; set; }

        public string Adress { get; set; }
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        [Required]
        public List<ExamScheduleElement> ExamScheduleElements { get; set; }

        public University() {
            Name = "";
            Adress = "";
            ExamScheduleElements = new List<ExamScheduleElement>();
        }
    }
}
