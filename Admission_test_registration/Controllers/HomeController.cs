using Admission_test_registration.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace Admission_test_registration.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        ExamDBContext _context = new ExamDBContext();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public ActionResult SpecificError(string message)
        {
            ViewData["Message"] = message;
            return View();
        }
        public IActionResult Map()
        {
            var universities = _context.Universities.Include(u => u.ExamScheduleElements).ToList();
            universities.ForEach(u => u.ExamScheduleElements.ForEach(el => el.University = null));
            ViewBag.Universities = universities;
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        
        }
    }
