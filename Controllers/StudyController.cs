using Microsoft.AspNetCore.Mvc;

namespace LibrarySite.Controllers
{
    public class StudyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
