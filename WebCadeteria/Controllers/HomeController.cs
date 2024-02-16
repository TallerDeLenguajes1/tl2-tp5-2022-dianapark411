using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebCadeteria.Models;

namespace WebCadeteria.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {   
        //Usuario no logueados: Pantalla de logueo
        if (!esta_logueado()) {
            return RedirectToRoute(new { controller = "Sesion", action = "Index" });
        }
              
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    private bool esta_logueado(){
        return HttpContext.Session.Keys.Any();
    }
    private bool es_admin()
    {
        return HttpContext.Session.Keys.Any() && HttpContext.Session.GetString("Rol") == "Administrador";
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
