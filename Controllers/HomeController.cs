using App.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Controllers;

public class HomeController : Controller
{
    private DataContext db;

    public HomeController(DataContext db)
    {
        this.db = db;
    }

    public IActionResult Index()
    {
        var galerias = db.Galerias
            .Include(g => g.Imagens)
            .AsNoTracking().ToList();
        return View(galerias);
    }

    public IActionResult Portfolio()
    {
        return View();
    }

    public IActionResult Sobre()
    {
        return View();
    }
}