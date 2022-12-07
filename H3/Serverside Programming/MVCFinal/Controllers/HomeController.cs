using System.Diagnostics;
using System.Dynamic;
using Microsoft.AspNetCore.Mvc;
using MVCFinal.Models;

using Microsoft.EntityFrameworkCore;
namespace MVCFinal.Controllers;

public class HomeController : Controller
{
    private readonly DatabaseContext _context;

    public HomeController(DatabaseContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        dynamic model = new ExpandoObject();

        model.Clients = _context.Client.ToList();
        model.Products =_context.Product.ToList();
        model.Cases = _context.Case.Include(c => c.Resources).ToList();
        model.ResourceTasks = _context.ResourceTask.ToList();


        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
