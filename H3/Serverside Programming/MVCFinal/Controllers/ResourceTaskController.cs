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
    public class ResourceTaskController : Controller
    {
        private readonly DatabaseContext _context;

        public ResourceTaskController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: ResourceTask
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.ResourceTask.Include(r => r.Case);
            return View(await databaseContext.ToListAsync());
        }

        // GET: ResourceTask/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ResourceTask == null)
            {
                return NotFound();
            }

            var resourceTask = await _context.ResourceTask
                .Include(r => r.Case)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (resourceTask == null)
            {
                return NotFound();
            }

            return View(resourceTask);
        }

        // GET: ResourceTask/Create
        public IActionResult Create()
        {
            ViewData["CaseId"] = new SelectList(_context.Case, "Id", "Id");
            return View();
        }

        // POST: ResourceTask/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Status,Description,CaseId")] ResourceTask resourceTask)
        {
            if (ModelState.IsValid)
            {
                _context.Add(resourceTask);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CaseId"] = new SelectList(_context.Case, "Id", "Id", resourceTask.CaseId);
            return View(resourceTask);
        }

        // GET: ResourceTask/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ResourceTask == null)
            {
                return NotFound();
            }

            var resourceTask = await _context.ResourceTask.FindAsync(id);
            if (resourceTask == null)
            {
                return NotFound();
            }
            ViewData["CaseId"] = new SelectList(_context.Case, "Id", "Id", resourceTask.CaseId);
            return View(resourceTask);
        }

        // POST: ResourceTask/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Status,Description,CaseId")] ResourceTask resourceTask)
        {
            if (id != resourceTask.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(resourceTask);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResourceTaskExists(resourceTask.Id))
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
            ViewData["CaseId"] = new SelectList(_context.Case, "Id", "Id", resourceTask.CaseId);
            return View(resourceTask);
        }

        // GET: ResourceTask/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ResourceTask == null)
            {
                return NotFound();
            }

            var resourceTask = await _context.ResourceTask
                .Include(r => r.Case)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (resourceTask == null)
            {
                return NotFound();
            }

            return View(resourceTask);
        }

        // POST: ResourceTask/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ResourceTask == null)
            {
                return Problem("Entity set 'DatabaseContext.ResourceTask'  is null.");
            }
            var resourceTask = await _context.ResourceTask.FindAsync(id);
            if (resourceTask != null)
            {
                _context.ResourceTask.Remove(resourceTask);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResourceTaskExists(int id)
        {
          return (_context.ResourceTask?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
