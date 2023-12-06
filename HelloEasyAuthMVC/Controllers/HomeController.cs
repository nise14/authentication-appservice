using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HelloEasyAuthMVC.Models;

namespace HelloEasyAuthMVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        ViewData["IsAuthenticated"] = User.Identity?.IsAuthenticated;
        ViewData["Name"] = User.Identity?.Name;


        var headers = Request.Headers.Select(h => new KeyValuePair<string, string>(h.Key, Convert.ToString(h.Value)));
        return View(headers);
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
