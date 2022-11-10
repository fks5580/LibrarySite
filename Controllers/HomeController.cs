using LibrarySite.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LibrarySite.Controllers
{
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult LoginSuccess()
        {
            return View();
        }

    }
}