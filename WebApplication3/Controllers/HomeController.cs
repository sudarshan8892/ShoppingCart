using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication3.ModelForView;
using WebApplication3.Models;


namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeRepository  homeRepository;
        public HomeController(ILogger<HomeController> logger,IHomeRepository repository)
        {
            _logger = logger;
            homeRepository = repository;
        }

        
        public IActionResult Index(string str = "", int GenereID = 0)
        {
           
            var generlist= homeRepository.GenresList(); 
            var booklist = homeRepository.getBooks(str, GenereID);
            ModelView modelFor = new ModelView
            {
             books= booklist,
             genres= generlist,
             str=str,
             GenereID= GenereID
            };
            return View(modelFor);
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