using Microsoft.AspNetCore.Mvc;
using MVCApp.Models;
using System.Diagnostics;

namespace MVCApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private static Calculation? _calculation = null;

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

        public IActionResult Math()
        {
            _logger.LogInformation("Math() action called");
            Random random = new Random();
            _calculation = new(new int[] { random.Next(1, 11), random.Next(1, 11) }, (Operator)random.Next(3));

            _logger.LogDebug(_calculation.ToString());

            return View(_calculation);
        }

        public IActionResult MathValidate(IFormCollection keyValuePairs)
        {

            _logger.LogInformation("MathValidate() action called");

            //int answer = Convert.ToInt32(keyValuePairs["Answer"]);

            //_calculation.Answer = answer;

            //_logger.LogInformation(_calculation.ToString());

            //return View(_calculation);


            try
            {
                int answer = Convert.ToInt32(keyValuePairs["Answer"]);

                _calculation.Answer = answer;


                _logger.LogInformation(_calculation.ToString());

                return View(_calculation);

            } catch (NullReferenceException exception){
                _logger.LogError(exception.Message);
            }

            _logger.LogDebug(_calculation.ToString());
            return View(_calculation);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
