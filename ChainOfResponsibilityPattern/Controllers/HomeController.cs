using ChainOfResponsibilityPattern.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ChainOfResponsibilityPattern.ChainOfResponsibility;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChainOfResponsibilityPattern.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppIdenitytDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppIdenitytDbContext context)
        {
            _logger = logger;
            _context = context;
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

        public async Task<IActionResult> SendEmail()
        {
            var products= await _context.Products.ToListAsync();
            var excelProcessHandler = new ExcelProcessHandler<Product>();
            var zipFileProcessHandler = new ZipProcessHandler<Product>();
            var emailProcessHandler = new EmailProcessHandler<Product>();
            excelProcessHandler.SetNext(zipFileProcessHandler).SetNext(emailProcessHandler);
            excelProcessHandler.Handle(products);
            return RedirectToAction("Index");
        }
    }
}