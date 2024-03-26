using Admission_test_registration.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.ConstrainedExecution;

namespace Admission_test_registration.Controllers
{
    
    public class CertificateController : Controller
    {
        public ExamDBContext _context { get; set; } = new ExamDBContext();

        // GET: CertificateController/Create
        public ActionResult Create(int studentId)
        {
            var educators = _context.Educators.ToList();
            if(educators.Count() == 0)
            {
                string msg = "Oooops, looks like educators are not added to the system, You can not add certificate for now. Try later";
                return RedirectToAction("SpecificError", "Home", new { message = msg });
            }
            var student = _context.Students.Find(studentId);
            if (student == null)
            {
                string msg = "Oooops, looks like student-owner of the certificate has been removed from System, You can not add certificate for now. Try later";
                return RedirectToAction("SpecificError", "Home", new { message = msg });
            }

            ViewBag.Educators = educators;
            ViewBag.Student = student;
            ViewBag.StudentId = student.StudentId;

            return View();
        }

        // POST: CertificateController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Certificate cerf)
        {
            var alreadyInDB = _context.Certificates.FirstOrDefault(c => c.StudentId == cerf.StudentId);
            if (alreadyInDB != null)
            {
                string msg = "Certificate is NOT added. Student already has one.";
                return RedirectToAction("SpecificError", "Home", new { message = msg });
                //return RedirectToAction("SpecificError", new { message =  msg});
            }
            int yearDifference = DateOnly.FromDateTime(DateTime.Today).Year - cerf.ReceiveDate.Year;
            if (yearDifference > 4)
            {
                string msg = "Certificate is not valid. It should be received max 4 years ago.";
                return RedirectToAction("SpecificError", "Home", new { message = msg });
            }
            if (!ModelState.IsValid)
            {
                string msg = "Certificate is NOT added. Input data is NOT valid.";
                return RedirectToAction("SpecificError", "Home", new { message = msg });
                //ViewBag.Student = _context.Students.Find(cerf.StudentId);
                //return View(cerf);
            }
            var student = _context.Students.FirstOrDefault(s => s.StudentId == cerf.StudentId);
            var educator = _context.Educators.FirstOrDefault(e => e.EducatorId == cerf.EducatorId);
            
            student.Certificate = cerf;
            educator.CertificateList.Add(cerf);

            cerf.Educator = educator;
            cerf.Student = student;

            _context.Certificates.Add(cerf);
            _context.Students.Update(student);
            _context.Educators.Update(educator);

            _context.SaveChanges();

            var confirmCerf = _context.Certificates.FirstOrDefault(c => c.StudentId == cerf.StudentId);
            if (confirmCerf == null)
            {
                string msg = "Ooop somehthing went horribly wrong, try later";
                return RedirectToAction("SpecificError", "Home", new { message = msg });
            }
            return Redirect("/Student/Details/" + confirmCerf.StudentId);
        }

        // GET: CertificateController/Edit/5
        public ActionResult Edit(int id)
        {
            var educators = _context.Educators.ToList();
            if (educators.Count() == 0)
            {
                string msg = "Oooops, looks like educators are not added to the system," +
                    " You can not edit certificate for now. Try later";
                return RedirectToAction("SpecificError", "Home", new { message = msg });
            }
            var student = _context.Students.Find(id);
            if (student == null)
            {
                string msg = "Oooops, looks like student-owner of the certificate has been removed from System," +
                    " You can not edit certificate for now. Try later";
                return RedirectToAction("SpecificError", "Home", new { message = msg });
            }
            var oldCerf = _context.Certificates.FirstOrDefault(c => c.StudentId == id);
            if (oldCerf == null)
            {
                string msg = "Oooops, looks like this certificate has been removed from System," +
                    " You can not add certificate for now. Try later";
                return RedirectToAction("SpecificError", "Home", new { message = msg });
            }
            ViewBag.Educators = educators;
            ViewBag.oldCerf = oldCerf;

            return View();
        }

        // POST: CertificateController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Certificate cerf)
        {
            var editSerf = _context.Certificates.Find(cerf.CertificateId);
            if (editSerf == null)
            {
                string msg = "Oooops, looks like current certificate has been removed from System," +
                    " You can not edit certificate for now. Try later";
                return RedirectToAction("SpecificError", "Home", new { message = msg });
            }

            int yearDifference = DateOnly.FromDateTime(DateTime.Today).Year - cerf.ReceiveDate.Year;
            if (yearDifference > 4)
            {
                string msg = "Certificate is not valid. It should be received max 4 years ago.";
                return RedirectToAction("SpecificError", "Home", new { message = msg });
            }
            if (!ModelState.IsValid)
            {
                string msg = "Certificate is NOT added. Input data is NOT valid.";
                return RedirectToAction("SpecificError", "Home", new { message = msg });
            }
          
            var educator = _context.Educators.FirstOrDefault(e => e.EducatorId == cerf.EducatorId);
            editSerf.UpdateFields(cerf);
            editSerf.Educator = educator;
            _context.Certificates.Update(editSerf);
            _context.SaveChanges();

            var confirmCerf = _context.Certificates.FirstOrDefault(c => c.StudentId == cerf.StudentId);
            if (confirmCerf == null)
            {
                string msg = "Ooop somehthing went horribly wrong, try later";
                return RedirectToAction("SpecificError", "Home", new { message = msg });
            }
            return Redirect("/Student/Details/" + confirmCerf.StudentId);
        }

        // GET: CertificateController/Delete/5
        public ActionResult Delete(int id)
        {
            var cerf = _context.Certificates.Find(id);
            if (cerf == null)
            {
                string msg = "Certificate has been already removed from Database.";
                return RedirectToAction("SpecificError", "Home", new { message = msg });
            }

            _context.Certificates.Remove(cerf);
            _context.SaveChanges();
            return Redirect("/Student/Details/" + cerf.StudentId);
        }

        // POST: CertificateController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePostMethod(int id)
        {
            return RedirectToAction("Delete", new { id = id });
        }
    }
}
