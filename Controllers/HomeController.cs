using Microsoft.AspNetCore.Mvc;
using Pearson_CodingChallenge.Models;
using Pearson_CodingChallenge.Services;
using Pearson_CodingChallenge.Utility;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upload(DataFile file)
        {
            try
            {
                //TODO: Process File Upload

                if(file.ImportFile != null)
                {
                    FileProcessor.ProcessFile(file);
                }

                return RedirectToAction(nameof(Upload));
            }
            catch (Exception ex)
            {
                Console.WriteLine("HomeController :: Upload (Post) :: Something went wrong in the file processing" + ex);
                return View();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
