namespace Admission_test_registration.Models
{
    public class StudentFull
    {
        public Student ?Student { get; set; }
        public Certificate ?Certificate { get; set; }
        public RegistrationSheet ?RegistrationSheet { get; set; }
    }
}
