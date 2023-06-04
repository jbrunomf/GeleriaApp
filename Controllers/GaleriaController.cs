using App.Data;
using App.Models;
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


        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(Galeria galeria)
        {
            if (ModelState.IsValid)
            {
                db.Galerias.Add(galeria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(galeria);
        }

        [HttpGet]
        public IActionResult Alterar(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var galeria = db.Galerias.Find(id);
            if (galeria == null)
            {
                return NotFound();
            }
            return View(galeria);
        }

        [HttpPost]
        public IActionResult Alterar(Galeria galeria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(galeria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(galeria);
        }
    }
}
