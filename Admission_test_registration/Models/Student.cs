using System.ComponentModel.DataAnnotations;

namespace Admission_test_registration.Models
{
    public class Student : PersonInterface
    {
        public int StudentId { get; set; }

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
        [DateTimeValidation(18, 60)]
        public DateOnly DateOfBirth { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public Sex Sex { get; set; }
        public Certificate? Certificate { get; set; }
        public RegistrationSheet? RegistrationSheet { get; set; }
        public Student() {}
        public void UpdateFields(Student Newstudent)
        {
            Name = Newstudent.Name;
            Adress = Newstudent.Adress;
            DateOfBirth = Newstudent.DateOfBirth;
            telNumber = Newstudent.telNumber;
            Email = Newstudent.Email;
            Sex = Newstudent.Sex;
        }
        public double PredictGrade()
        {
            //Load sample data
            var sampleData = new ExamGradePredictor.ModelInput()
            {
                Sex = Sex.ToString(),
                Math_Grade = Certificate.MathGrade,
                History_Grade = Certificate.HistoryGrade,
                English_Grade = Certificate.EnglishGrade,
                Social_Activity = Certificate.SocialActivity,
                Science_Competition_Activity = Certificate.CompetitionActivity,
                Sport_Activity = Certificate.SportActivity
            };
            //Load sample data
            //Load model and predict output
            var result = ExamGradePredictor.Predict(sampleData);

            //Load model and predict output
            return result.Score;
        }
    }
}
