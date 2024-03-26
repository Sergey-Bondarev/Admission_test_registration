using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text;
using Microsoft.AspNetCore.Components.Forms;

namespace Admission_test_registration.Models
{
    public class ExamDBContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Educator> Educators { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<RegistrationSheet> RegistrationSheets { get; set; }
        public DbSet<ExamScheduleElement> ExamScheduleElements { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public ExamDBContext()
        {
            //Database.EnsureCreated();
            //Database.EnsureDeleted();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var appDomSring = AppDomain.CurrentDomain.BaseDirectory;
                int binIndex = appDomSring.LastIndexOf("\\bin");

                string dbPath = appDomSring.Substring(0, binIndex);
                var databaseFilePath = Path.Combine(dbPath, "DataBase", "ExamRegistrationDB.mdf");
   
                optionsBuilder.UseSqlServer($"Server=(localdb)\\mssqllocaldb;Database=ExamRegistrationDB;AttachDbFilename={databaseFilePath};TrustServerCertificate=true");
   
            }
            //optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ExamRegistrationDB;Integrated Security=true;TrustServerCertificate=true");
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Certificate)
                .WithOne(c => c.Student)
                .HasForeignKey<Certificate>(c => c.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Certificate>()
                .HasOne(c => c.Student)
                .WithOne(s => s.Certificate)
                .HasForeignKey<Certificate>(c => c.StudentId);
            

            modelBuilder.Entity<Student>()
                .HasOne(s => s.RegistrationSheet)
                .WithOne(r => r.Student)
                .HasForeignKey<RegistrationSheet>(r => r.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RegistrationSheet>()
                .HasOne(r => r.Student)
                .WithOne(s => s.RegistrationSheet)
                .HasForeignKey<RegistrationSheet>(r => r.StudentId);
            

            modelBuilder.Entity<Educator>()
                .HasMany(e => e.CertificateList)
                .WithOne(c => c.Educator)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Certificate>()
                .HasOne(c => c.Educator)
                .WithMany(e => e.CertificateList);
        }

        public void FirstInitializeDB()
        {
            using (ExamDBContext db = new ExamDBContext())
            //#nullable disable warnings
            {
                Student s1 = new Student
                {
                    Name = "Serhio",
                    telNumber = "+380111111111",
                    Email = "stmail@nure.ua",
                    Adress = "St Test-street 1",
                    Sex = Sex.Male,
                    DateOfBirth = new DateOnly(2003, 8, 17)
                };
                Student s2 = new Student
                {
                    Name = "Ann",
                    telNumber = "+380222222222",
                    Email = "stmail2@nure.ua",
                    Adress = "St Test-street 2",
                    Sex = Sex.Female,
                    DateOfBirth = new DateOnly(2004, 2, 19)
                };
                Student s3 = new Student
                {
                    Name = "Geogre",
                    telNumber = "+380333333333",
                    Email = "geogre@nure.ua",
                    Adress = "St Test-street 3",
                    Sex = Sex.Male,
                    DateOfBirth = new DateOnly(2002, 11, 1)
                };
                Student s4 = new Student
                {
                    Name = "Silvia",
                    telNumber = "+380444444444",
                    Email = "silvia@nure.ua",
                    Adress = "St Test-street 4",
                    Sex = Sex.Female,
                    DateOfBirth = new DateOnly(2003, 6, 15)
                };
                db.Students.Add(s1);
                db.Students.Add(s2);
                db.Students.Add(s3);
                db.Students.Add(s4);
                db.SaveChanges();

                Educator ed1 = new Educator
                {
                    Name = "Julia",
                    telNumber = "+380333333344",
                    Email = "tmail1@nure.ua",
                    Adress = "T Test-street 1",
                    Sex = Sex.Female,
                    Degree = 3,
                    DateOfBirth = new DateOnly(1990, 5, 3)

                };
                Educator ed2 = new Educator
                {
                    Name = "Andrew",
                    telNumber = "+380444444455",
                    Email = "tmail2@nure.ua",
                    Adress = "T Test-street 2",
                    Sex = Sex.Male,
                    Degree = 5,
                    DateOfBirth = new DateOnly(1981, 11, 9)
                };
                Educator ed3 = new Educator
                {
                    Name = "Maya",
                    telNumber = "+380444444466",
                    Email = "maya@nure.ua",
                    Adress = "T Test-street 22",
                    Sex = Sex.Male,
                    Degree = 5,
                    DateOfBirth = new DateOnly(1989, 5, 17)
                };
                db.Educators.Add(ed1);
                db.Educators.Add(ed2);
                db.Educators.Add(ed3);
                db.SaveChanges();

                University un1 = new University
                {
                    Name = "Kharkiv National University of Radio Electronics",
                    Adress = "Kharkiv, Nauky Ave 14",
                    Latitude = 50.015348760653254,
                    Longitude = 36.227343108173876,
                    ExamScheduleElements = new List<ExamScheduleElement>()
                };
                University un2 = new University
                {
                    Name = "Kyiv Polytechnic Institute",
                    Adress = "Kiev, Beresteiskyi Ave 37",
                    Latitude = 50.44898957156887,
                    Longitude = 30.456998462941247,
                    ExamScheduleElements = new List<ExamScheduleElement>()
                };
                University un3 = new University
                {
                    Name = "National University of Lviv",
                    Adress = "Lviv, Universytetska St 1",
                    Latitude = 49.840419841141845,
                    Longitude = 24.02229142714522,
                    ExamScheduleElements = new List<ExamScheduleElement>()
                };
                db.Universities.Add(un1);
                db.Universities.Add(un2);
                db.Universities.Add(un3);
                db.SaveChanges();

                Certificate c1 = new Certificate
                {
                    Student = db.Students.ElementAt(0),
                    Educator = db.Educators.ElementAt(0),
                    MathGrade = 94,
                    EnglishGrade = 91,
                    HistoryGrade = 88,
                    SocialActivity = true,
                    SportActivity = true,
                    CompetitionActivity = true,
                    ReceiveDate = new DateOnly(2024, 3, 15)
                };
                Certificate c2 = new Certificate
                {
                    Student = db.Students.ElementAt(1),
                    Educator = db.Educators.ElementAt(1),
                    MathGrade = 75,
                    EnglishGrade = 85,
                    HistoryGrade = 90,
                    SocialActivity = true,
                    SportActivity = false,
                    CompetitionActivity = false,
                    ReceiveDate = new DateOnly(2024, 3, 11)
                };
                db.Certificates.Add(c1);
                db.Certificates.Add(c2);
                db.SaveChanges();

                ExamScheduleElement ex1 = new ExamScheduleElement
                {
                    University = db.Universities.ElementAt(0),
                    ExamTime = new DateTime(2024, 3, 16, 10, 0, 0)
                };
                ExamScheduleElement ex2 = new ExamScheduleElement
                {
                    University = db.Universities.ElementAt(0),
                    ExamTime = new DateTime(2024, 4, 16, 12, 0, 0)
                };
                ExamScheduleElement ex3 = new ExamScheduleElement
                {
                    University = db.Universities.ElementAt(1),
                    ExamTime = new DateTime(2024, 3, 16, 10, 0, 0)
                };
                ExamScheduleElement ex4 = new ExamScheduleElement
                {
                    University = db.Universities.ElementAt(1),
                    ExamTime = new DateTime(2024, 4, 16, 12, 0, 0)
                };
                ExamScheduleElement ex5 = new ExamScheduleElement
                {
                    University = db.Universities.ElementAt(2),
                    ExamTime = new DateTime(2024, 3, 25, 13, 0, 0)
                };
                ExamScheduleElement ex6 = new ExamScheduleElement
                {
                    University = db.Universities.ElementAt(2),
                    ExamTime = new DateTime(2024, 3, 25, 16, 0, 0)
                };
                db.ExamScheduleElements.AddRange(new List<ExamScheduleElement>() { ex1, ex2, ex3, ex4, ex5, ex6 });
                db.SaveChanges();

                RegistrationSheet r1 = new RegistrationSheet
                {
                    Student = db.Students.ElementAt(0),
                    RegistrationTime = DateTime.Now,
                    ExamScheduleElement = db.ExamScheduleElements.ElementAt(0)
                };
                RegistrationSheet r2 = new RegistrationSheet
                {
                    Student = db.Students.ElementAt(1),
                    RegistrationTime = DateTime.Now,
                    ExamScheduleElement = db.ExamScheduleElements.ElementAt(3)
                };
                db.RegistrationSheets.Add(r1);
                db.RegistrationSheets.Add(r2);

                db.SaveChanges();
            }
        }
    }
}
