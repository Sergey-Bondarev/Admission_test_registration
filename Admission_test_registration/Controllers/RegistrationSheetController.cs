using Admission_test_registration.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Runtime.ConstrainedExecution;

namespace Admission_test_registration.Controllers
{
    public class RegistrationSheetController : Controller
    {
        public ExamDBContext _context = new ExamDBContext();

        // GET: RegistrationSheetController/Create
        public ActionResult Create(int id)
        {
            var unies = _context.Universities.ToList();
            var shedule = _context.ExamScheduleElements.ToList();
            if(unies.Count() == 0 || shedule.Count() == 0)
            {
                string msg = "Oooops, looks like universities or shedule are not added to the system," +
                    " You can not pass registration for now. Try later";
                return RedirectToAction("SpecificError", "Home", new { message = msg });
            }
            var student = _context.Students.Find(id);
            if (student == null)
            {
                string msg = "Oooops, looks like current student was removed from system," +
                    " You can not pass registration for now. Try later";
                return RedirectToAction("SpecificError", "Home", new { message = msg });
            }
            shedule.ForEach(s => s.University = null);
            var json = JsonConvert.SerializeObject(shedule);
            ViewBag.JsN = json;
            ViewBag.ExamScheduleElements = shedule;
            ViewBag.Universities = unies;
            ViewBag.Student = student;
            return View();
        }

        // POST: RegistrationSheetController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RegistrationSheet registrationSheet)
        {
            if (!ModelState.IsValid)
            {
                string msg = "Registration Sheet is NOT added. Input data is NOT valid.";
                return RedirectToAction("SpecificError", "Home", new { message = msg });
            }
            var student = _context.Students.FirstOrDefault(s => s.StudentId == registrationSheet.StudentId);
            registrationSheet.Student = student;

            var shedule = _context.ExamScheduleElements.FirstOrDefault(e => e.ExamScheduleElementId == registrationSheet.ExamScheduleElementId);
            registrationSheet.ExamScheduleElement = shedule;

            _context.RegistrationSheets.Add(registrationSheet);
            _context.SaveChanges();

            var confirm = _context.RegistrationSheets.FirstOrDefault(r => r.StudentId == registrationSheet.StudentId);
            if (confirm == null)
            {
                string msg = "Ooop somehthing went horribly wrong, try later";
                return RedirectToAction("SpecificError", "Home", new { message = msg });
            }
            return Redirect("/Student/Details/" + confirm.StudentId);
        }

        // GET: RegistrationSheetController/Delete/5
        public ActionResult Delete(int id)
        {
            var regist = _context.RegistrationSheets.Find(id);
            if (regist == null)
            {
                string msg = "Registration sheet has been already removed from Database.";
                return RedirectToAction("SpecificError", "Home", new { message = msg });
            }

            _context.RegistrationSheets.Remove(regist);
            _context.SaveChanges();
            return Redirect("/Student/Details/" + regist.StudentId);
        }

        // POST: RegistrationSheetController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePostMethod(int id)
        {
            return RedirectToAction("Delete", new { id = id });

        }
    }
}
