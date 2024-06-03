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
        private string message = "";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _dbService = new DatabaseService();
        }

        /* Orders View*/
        public IActionResult Index()
        {
            IEnumerable<Order> orders = _dbService.GetAllOrders();
            return View(orders);
        }

        /* Upload view*/
        public IActionResult Upload()
        {
            ViewData["Message"] = TempData["Message"];
            return View();
        }

        /**
         * Calls the action to process the uploaded file from the Upload view
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upload(DataFile file)
        {
            try
            {
                if(file.ImportFile != null)
                {
                    Dictionary<string, string> result = FileProcessor.ProcessFile(file);
                    if (result["errors"] != "")
                    {
                        TempData["Message"] = result["errors"];
                        Console.WriteLine(file.Message);
                    }
                    else
                    {
                        TempData["Message"] = result["message"];
                        Console.WriteLine(file.Message);
                    }
                }

                return RedirectToAction(nameof(Upload));
            }
            catch (Exception ex)
            {
                Console.WriteLine("HomeController :: Upload (Post) :: Something went wrong in the file processing" + ex);
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id)
        {
            /*
             * Meant to pass the id of the order selected to update the fulfilled status
             * The data is not being passed to this function so it doesn't work at the moment
             * If the data was passed this would use the stored procedure to update the order as fulfilled
             */
            try
            {
                Order dborder = _dbService.GetOrderById(id);
                if (dborder != null)
                {
                    dborder.IsFulfilled = true;
                    dborder.DateFulfilled = DateOnly.FromDateTime(DateTime.Now);
                    _dbService.UpdateOrder(dborder);
                }
            }
            catch( Exception ex)
            {
                Console.WriteLine("HomeController :: Edit :: Something Went Wrong :: " + ex.ToString());
            }

            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
