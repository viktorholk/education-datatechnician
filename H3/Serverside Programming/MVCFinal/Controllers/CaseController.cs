using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCFinal.Models;

namespace MVCFinal.Controllers
{
    public class CaseController : Controller
    {
        private readonly DatabaseContext _context;

        public CaseController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Case
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.Case.Include(i => i.Client).Include(i => i.Product);
            return View(await databaseContext.ToListAsync());
        }

        // GET: Case/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Case == null)
            {
                return NotFound();
            }

            var icase = await _context.Case
                .Include(i => i.Client)
                .Include(i => i.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (icase == null)
            {
                return NotFound();
            }

            return View(icase);
        }

        // GET: Case/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Id");
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id");
            return View();
        }

        // POST: Case/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,Description,ProductId,ClientId")] Case icase)
        {
            if (ModelState.IsValid)
            {
                _context.Add(icase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Id", icase.ClientId);
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id", icase.ProductId);
            return View(icase);
        }

        // GET: Case/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Case == null)
            {
                return NotFound();
            }

            var icase = await _context.Case.FindAsync(id);
            if (icase == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Id", icase.ClientId);
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id", icase.ProductId);
            return View(icase);
        }

        // POST: Case/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,Description,ProductId,ClientId")] Case icase)
        {
            if (id != icase.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(icase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CaseExists(icase.Id))
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
            ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Id", icase.ClientId);
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id", icase.ProductId);
            return View(icase);
        }

        // GET: Case/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Case == null)
            {
                return NotFound();
            }

            var icase = await _context.Case
                .Include(i => i.Client)
                .Include(i => i.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (icase == null)
            {
                return NotFound();
            }

            return View(icase);
        }

        // POST: Case/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Case == null)
            {
                return Problem("Entity set 'DatabaseContext.Case'  is null.");
            }
            var icase = await _context.Case.FindAsync(id);
            if (icase != null)
            {
                _context.Case.Remove(icase);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CaseExists(int id)
        {
          return (_context.Case?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
