using Microsoft.AspNetCore.Mvc;
using Pearson_CodingChallenge.Models;
using Pearson_CodingChallenge.Services;
using System.Diagnostics;

namespace Pearson_CodingChallenge.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private DatabaseService _dbService = null;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _dbService = new DatabaseService();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upload()
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
