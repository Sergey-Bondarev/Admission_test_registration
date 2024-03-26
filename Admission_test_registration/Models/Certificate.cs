using System.ComponentModel.DataAnnotations;

namespace Admission_test_registration.Models
{
    public class Certificate
    {
        public int CertificateId { get; set; }
        public Student? Student { get; set; }
        public int StudentId { get; set; }
        public Educator? Educator { get; set; }
        public int EducatorId { get; set; }

        [Required]
        [Range(60, 100)]
        public int MathGrade { get; set; }

        [Required]
        [Range(60, 100)]
        public int EnglishGrade { get; set; }

        [Required]
        [Range(60, 100)]
        public int HistoryGrade { get; set; }

        [Required]
        public bool SocialActivity { get; set; }

        [Required]
        public bool CompetitionActivity { get; set; }

        [Required]
        public bool SportActivity { get; set; }

        [Required]
        [DateTimeValidation(0, 4)]
        public DateOnly ReceiveDate {  get; set; }

        public Certificate(){
        }
        public void UpdateFields(Certificate newCertificate)
        {
            ReceiveDate = newCertificate.ReceiveDate;
            EducatorId = newCertificate.EducatorId;
            SportActivity = newCertificate.SportActivity;
            CompetitionActivity = newCertificate.CompetitionActivity;
            SocialActivity = newCertificate.SocialActivity;
            MathGrade = newCertificate.MathGrade;
            HistoryGrade = newCertificate.HistoryGrade;
            EnglishGrade = newCertificate.EnglishGrade;

        }

    }
}
