using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCAppEntityFramework.Models;

namespace MVCAppEntityFramework.Controllers
{
    public class BookModelController : Controller
    {
        private readonly DatabaseContext _context;

        public BookModelController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: BookModel
        public async Task<IActionResult> Index()
        {
              return _context.BookModel != null ? 
                          View(await _context.BookModel.ToListAsync()) :
                          Problem("Entity set 'DatabaseContext.BookModel'  is null.");
        }

        // GET: BookModel/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BookModel == null)
            {
                return NotFound();
            }

            var bookModel = await _context.BookModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookModel == null)
            {
                return NotFound();
            }

            return View(bookModel);
        }

        // GET: BookModel/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BookModel/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ISBN,Title,Author,Publisher,Description,Category,TotalPages,Price")] BookModel bookModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bookModel);
        }

        // GET: BookModel/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BookModel == null)
            {
                return NotFound();
            }

            var bookModel = await _context.BookModel.FindAsync(id);
            if (bookModel == null)
            {
                return NotFound();
            }
            return View(bookModel);
        }

        // POST: BookModel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ISBN,Title,Author,Publisher,Description,Category,TotalPages,Price")] BookModel bookModel)
        {
            if (id != bookModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookModelExists(bookModel.Id))
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
            return View(bookModel);
        }

        // GET: BookModel/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BookModel == null)
            {
                return NotFound();
            }

            var bookModel = await _context.BookModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookModel == null)
            {
                return NotFound();
            }

            return View(bookModel);
        }

        // POST: BookModel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BookModel == null)
            {
                return Problem("Entity set 'DatabaseContext.BookModel'  is null.");
            }
            var bookModel = await _context.BookModel.FindAsync(id);
            if (bookModel != null)
            {
                _context.BookModel.Remove(bookModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookModelExists(int id)
        {
          return (_context.BookModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
