using System.Reflection.Metadata.Ecma335;
using App.Data;
using App.Models;
using App.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace App.Controllers
{
    public class ImagemController : Controller
    {
        private readonly DataContext db;

        private readonly IWebHostEnvironment env;

        private readonly IProcessadorImagem pi;

        public ImagemController(DataContext db, IWebHostEnvironment env, IProcessadorImagem pi)
        {
            this.db = db;
            this.env = env;
            this.pi = pi;
        }

        // GET: ImagemController
        public ActionResult Index(int? id)
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

            db.Entry(galeria).Collection(g => g.Imagens).Load();
            ViewBag.IdGaleria = id.Value;
            ViewBag.TituloGaleria = galeria.Titulo;
            return View(galeria.Imagens.ToList());
        }

        [HttpPost]
        public IActionResult Cadastrar(Imagem imagem)
        {
            if (ModelState.IsValid)
            {
                db.Imagens.Add(imagem);
                if (db.SaveChanges() > 0)
                {
                    string caminhoArquivoImagem = ObterCaminhoImagem("\\img\\", imagem.IdImagem, ".webp");
                    pi.SalvarUploadIagemAsync(caminhoArquivoImagem, imagem.ArquivoImagem).Wait();
                }

                return RedirectToAction("Index", "Imagem", new { id = imagem.IdGaleria });
            }

            return View(imagem);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            ViewBag.Galerias = new SelectList(db.Galerias.ToList(), "IdGaleria", "Titulo");
            return View();
        }

        private string ObterCaminhoImagem(string pastaImagens, int idImagem, string extensao)
        {
            // <APPDIR>/wwwroot/imagens
            string caminhopastaImagens = env.WebRootPath + pastaImagens;
            var nomeArquivo = $"{idImagem:D6}{extensao}";
            var caminhoArquivoImagem = Path.Combine(caminhopastaImagens, nomeArquivo);
            return caminhoArquivoImagem;
        }

        public IActionResult Alterar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imagem = db.Imagens.Find(id);
            if (imagem == null)
            {
                return NotFound();
            }

            return View(imagem);

        }

        [HttpPost]
        public IActionResult Alterar(Imagem imagem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(imagem).State = EntityState.Modified;
                if (db.SaveChanges() > 0)
                {
                    if (imagem.ArquivoImagem != null)
                    {
                        string caminhoArquivoImagem = ObterCaminhoImagem("\\img\\", imagem.IdImagem, ".webp");
                        pi.SalvarUploadIagemAsync(caminhoArquivoImagem, imagem.ArquivoImagem).Wait();
                    }
                }

                return RedirectToAction("Index", new { id = imagem.IdGaleria });
            }

            return View(imagem);
        }

        [HttpGet]
        public IActionResult Excluir(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imagem = db.Imagens.Find(id);
            if (imagem == null)
            {
                return NotFound();
            }

            return View(imagem);
        }

        [HttpPost]
        public IActionResult Excluir(int id)
        {
            var imagem = db.Imagens.Find(id);
            if (imagem == null)
            {
                return NotFound();
            }

            db.Imagens.Remove(imagem);
            db.SaveChanges();
            return RedirectToAction("Index", controllerName: "Galeria");
        }
    }
}