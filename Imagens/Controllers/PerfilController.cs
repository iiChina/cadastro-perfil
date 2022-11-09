using Imagens.Data;
using Imagens.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Imagens.Controllers
{
    public class PerfilController : Controller
    {
        private readonly Context _appContext;

        public PerfilController(Context appContext)
        {
            _appContext = appContext;
        }

        public async Task<IActionResult> Index()
        {
            var allPerfis = await _appContext.Perfis.ToListAsync();
            return View(allPerfis);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Perfil perfil, IList<IFormFile> Img)
        {
            IFormFile uploadedImage = Img.FirstOrDefault();
            MemoryStream ms = new MemoryStream();
            if(Img.Count > 0)
            {
                uploadedImage.OpenReadStream().CopyTo(ms);
                perfil.Foto = ms.ToArray();
            }

            _appContext.Perfis.Add(perfil);
            _appContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
                return NotFound();

            var perfil = await _appContext.Perfis.FindAsync(id);

            if (perfil == null)
                return BadRequest();

            return View(perfil);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
                return NotFound();

            var perfil = await _appContext.Perfis.FindAsync(id);

            if (perfil == null)
                return BadRequest();

            return View(perfil);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid? id, Perfil perfil, IList<IFormFile> Img)
        {
            if (id == null)
                return NotFound();

            var dadosAntigos = _appContext.Perfis.AsNoTracking().FirstOrDefault(p => p.Id == id);

            var uploadedImage = Img.FirstOrDefault();
            MemoryStream ms = new MemoryStream();
            if (Img.Count > 0)
            {
                uploadedImage.OpenReadStream().CopyTo(ms);
                perfil.Foto = ms.ToArray();
            }
            else
            {
                perfil.Foto = dadosAntigos.Foto;
            }

            if (ModelState.IsValid)
            {
                _appContext.Perfis.Update(perfil);
                await _appContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(perfil);   
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
                return NotFound();

            var perfil = await _appContext.Perfis.FindAsync(id);

            if (perfil == null)
                return BadRequest();

            return View(perfil);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid? id)
        {
            var perfil = await _appContext.Perfis.FindAsync(id);
            _appContext.Perfis.Remove(perfil);
            await _appContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
