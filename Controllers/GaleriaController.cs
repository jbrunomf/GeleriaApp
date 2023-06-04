using App.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Controllers
{
    public class GaleriaController : Controller
    {
        private readonly DataContext db;

        public GaleriaController(DataContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            var galerias = db.Galerias.AsNoTracking().ToList();
            return View(galerias);
        }
    }
}
