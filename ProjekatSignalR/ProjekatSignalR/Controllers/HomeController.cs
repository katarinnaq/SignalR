using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjekatSignalR.Models;
using System.Diagnostics;

namespace ProjekatSignalR.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // ovo vraca id trenutno ulogovanog korisnika
            ViewBag.UserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
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
    }
}
