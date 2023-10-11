using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nøsted.Data;
using Nøsted.Models;

namespace Nøsted.Controllers
{
    public class OrdreController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdreController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ordre
        public async Task<IActionResult> Index()
        {
              return _context.Ordre != null ? 
                          View(await _context.Ordre.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Ordre'  is null.");
        }

        // GET: Ordre/SøkOrdre
        public async Task<IActionResult> SøkOrdre()
        {
            return View();
        }

        // GET: Ordre/ResultatOrdre
        public async Task<IActionResult> ResultatOrdre(string Status, string SearchPhrase)
        {
            IQueryable<Ordre> query = _context.Ordre;

            if (!string.IsNullOrEmpty(Status))
            {
                Status = Status.ToLower(); // Konverter til små bokstaver
                query = query.Where(o => o.OrdreStatus != null && o.OrdreStatus.ToLower() == Status);
            }

            if (!string.IsNullOrEmpty(SearchPhrase))
            {
                SearchPhrase = SearchPhrase.ToLower(); // Konverter til små bokstaver
                query = query.Where(o => o.OrdreInformasjon.ToLower().Contains(SearchPhrase));
            }

            return View("Index", await query.ToListAsync());
        }






        // GET: Ordre/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ordre == null)
            {
                return NotFound();
            }

            var ordre = await _context.Ordre
                .FirstOrDefaultAsync(m => m.OrdreId == id);
            if (ordre == null)
            {
                return NotFound();
            }

            return View(ordre);
        }

        // GET: Ordre/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ordre/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrdreId,OrdreStatus,OrdreInformasjon")] Ordre ordre)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ordre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ordre);
        }

        // GET: Ordre/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ordre == null)
            {
                return NotFound();
            }

            var ordre = await _context.Ordre.FindAsync(id);
            if (ordre == null)
            {
                return NotFound();
            }
            return View(ordre);
        }

        // POST: Ordre/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrdreId,OrdreStatus,OrdreInformasjon")] Ordre ordre)
        {
            if (id != ordre.OrdreId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ordre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdreExists(ordre.OrdreId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(ordre);
        }

        // GET: Ordre/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ordre == null)
            {
                return NotFound();
            }

            var ordre = await _context.Ordre
                .FirstOrDefaultAsync(m => m.OrdreId == id);
            if (ordre == null)
            {
                return NotFound();
            }

            return View(ordre);
        }

        // POST: Ordre/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ordre == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Ordre'  is null.");
            }
            var ordre = await _context.Ordre.FindAsync(id);
            if (ordre != null)
            {
                _context.Ordre.Remove(ordre);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdreExists(int id)
        {
          return (_context.Ordre?.Any(e => e.OrdreId == id)).GetValueOrDefault();
        }
    }
}
