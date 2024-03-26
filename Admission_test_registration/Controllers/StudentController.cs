using Admission_test_registration.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Buffers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Admission_test_registration.Controllers
{
    public class StudentController : Controller
    {
        public ExamDBContext _context { get; set; } = new ExamDBContext();
        public ActionResult Index()
        {
            return View(_context.Students.ToList());
        }
        public ActionResult Details(int id)
        {
            var studentModel = new StudentFull {
                Student = _context.Students.FirstOrDefault(s => s.StudentId == id),
                Certificate = _context.Certificates
                .Include(c => c.Educator)
                .FirstOrDefault(c => c.StudentId == id),
                RegistrationSheet = _context.RegistrationSheets
                .Include(r => r.ExamScheduleElement)
                .Include(r => r.ExamScheduleElement.University)
                .FirstOrDefault(s => s.StudentId == id)
            };
            return View(studentModel);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student student)
        {
            var alreadyInDB = _context.Students.Where(s => s.telNumber.Equals(student.telNumber));
            if(alreadyInDB.Count() > 0)
            {
                string msg = "Student is NOT added. Input Tel. Number already in use.";
                return RedirectToAction("SpecificError","Home", new { message = msg });
                //return RedirectToAction("SpecificError", new { message =  msg});
            }
            //int yearDifference = DateOnly.FromDateTime(DateTime.Today).Year - student.DateOfBirth.Year;
            //if (yearDifference < 18 || yearDifference > 60)
            //{
            //    string msg = "Student is NOT added. Person should be at least 18 years old and 60 max.";
            //    return RedirectToAction("SpecificError", "Home", new { message = msg });
            //}
            if (!ModelState.IsValid)
            {
                //string msg = "Student is NOT added. Input data is NOT valid.";
                //return RedirectToAction("SpecificError", "Home", new { message = msg });
                return View(student);
            }
            _context.Students.Add(new Student {
                Name = student.Name,
                Adress = student.Adress,
                telNumber = student.telNumber,
                Email = student.Email,
                Sex = student.Sex,
                DateOfBirth = student.DateOfBirth,
                Certificate = null,
                RegistrationSheet = null
            });
            _context.SaveChanges();
            var confirmSt = _context.Students.FirstOrDefault(s => s.telNumber == student.telNumber);
            if(confirmSt == null)
            {
                string msg = "Ooop somehthing went horribly wrong, try later";
                return RedirectToAction("SpecificError", "Home", new { message = msg });
            }
            return Redirect("/Student/Details/" + confirmSt.StudentId);
        }
        public ActionResult Edit(int id)
        {
            ViewBag.OldSt = _context.Students.FirstOrDefault(s => s.StudentId == id);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Student student)
        {
            var alreadyInDB = _context.Students.FirstOrDefault(s => s.StudentId == student.StudentId);
            if (alreadyInDB == null)
            {
                string msg = "Student is NOT edited. Looks like edited student was removed from Database, you need to recreate it.";
                return RedirectToAction("SpecificError", "Home", new { message = msg });
            }
            int yearDifference = DateOnly.FromDateTime(DateTime.Today).Year - student.DateOfBirth.Year;
            if (yearDifference < 18 || yearDifference > 60)
            {
                string msg = "Student is NOT edited. Person should be at least 18 and max 60 years old.";
                return RedirectToAction("SpecificError", "Home", new { message = msg });
            }
            if (!ModelState.IsValid)
            {
                string msg = "Student is NOT edited. Input data is NOT valid.";
                return RedirectToAction("SpecificError", "Home", new { message = msg });
            }
            alreadyInDB.UpdateFields(student);
            _context.SaveChanges();
            var confirmSt = _context.Students.FirstOrDefault(s => s.StudentId == student.StudentId);
            if (confirmSt == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return Redirect("/Student/Details/" + confirmSt.StudentId);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null)
            {
                string msg = "Student is NOT edited. Looks like edited student was removed from Database, you need to recreate it.";
                return RedirectToAction("SpecificError", "Home", new { message = msg });
            }

            _context.Students.Remove(student);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePostMethod(int id)
        {
            return RedirectToAction("Delete", new { id = id });
        }
        [HttpPost]
        public ActionResult PredictGrade(int studentId)
        {
            var preparedStudents = _context.Students
                .Include(s => s.RegistrationSheet.ExamScheduleElement)
                .Include(s => s.Certificate)
                .Where(s => s.Certificate != null && s.RegistrationSheet != null).ToList();

            int uniId = preparedStudents.FirstOrDefault(s => s.StudentId == studentId).RegistrationSheet.ExamScheduleElement.UniversityId;
            
            var rating = PredictRatingOfUniversity(preparedStudents, uniId);
            ViewBag.Rate = rating.FindIndex(tuple => tuple.Item1 == studentId) + 1;
            ViewBag.NumberOfRegistration = rating.Count();
            ViewBag.Predicted = Math.Round(rating.FirstOrDefault(tuple => tuple.Item1 == studentId).Item2, 2);
            return PartialView("ExamResultPredict");
        }
        public List<Tuple<int, double>> PredictRatingOfUniversity(List<Student> preparedStudents,int uniId)
        {            
            var students = preparedStudents.Where(s => s.RegistrationSheet.ExamScheduleElement.UniversityId == uniId).ToList();
            List<Tuple<int, double>> studentIdTuples = new List<Tuple<int, double>>();

            foreach (var st in students)
            {
                double result = st.PredictGrade();
                studentIdTuples.Add(Tuple.Create(st.StudentId, result));
            }
            List<Tuple<int, double>> rating = studentIdTuples.OrderByDescending(tuple => tuple.Item2).ToList();
            return rating;
        }
    }
}
