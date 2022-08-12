using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KeluhKesahApp.Data;
using KeluhKesahApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace KeluhKesahApp.Controllers
{
    public class KeluhansController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KeluhansController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Keluhans
        public async Task<IActionResult> Index()
        {
            return _context.Keluhan != null ?
                        View(await _context.Keluhan.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Keluhan'  is null.");
        }

        // GET: Keluhans/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return _context.Keluhan != null ?
                        View() :
                        Problem("Entity set 'ApplicationDbContext.Keluhan'  is null.");
        }

        // PoST: Keluhans/ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(String SearchPhrase)
        {
            return View("Index", await _context.Keluhan.Where(j => j.Keluhanmu.Contains(SearchPhrase)).ToListAsync());
        }

        // GET: Keluhans/Details/5
       
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Keluhan == null)
            {
                return NotFound();
            }

            var keluhan = await _context.Keluhan
                .FirstOrDefaultAsync(m => m.Id == id);
            if (keluhan == null)
            {
                return NotFound();
            }

            return View(keluhan);
        }

        // GET: Keluhans/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Keluhans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Keluhanmu,Solusimu")] Keluhan keluhan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(keluhan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(keluhan);
        }

        // GET: Keluhans/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Keluhan == null)
            {
                return NotFound();
            }

            var keluhan = await _context.Keluhan.FindAsync(id);
            if (keluhan == null)
            {
                return NotFound();
            }
            return View(keluhan);
        }

        // POST: Keluhans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Keluhanmu,Solusimu")] Keluhan keluhan)
        {
            if (id != keluhan.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(keluhan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KeluhanExists(keluhan.Id))
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
            return View(keluhan);
        }

        // GET: Keluhans/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Keluhan == null)
            {
                return NotFound();
            }

            var keluhan = await _context.Keluhan
                .FirstOrDefaultAsync(m => m.Id == id);
            if (keluhan == null)
            {
                return NotFound();
            }

            return View(keluhan);
        }

        // POST: Keluhans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Keluhan == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Keluhan'  is null.");
            }
            var keluhan = await _context.Keluhan.FindAsync(id);
            if (keluhan != null)
            {
                _context.Keluhan.Remove(keluhan);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KeluhanExists(int id)
        {
          return (_context.Keluhan?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
