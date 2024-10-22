using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BOOK3.Data;
using BOOK3.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace BOOK3.Controllers
{
    public class Book31Controller : Controller
    {
        private readonly BOOK3Context _context;

        public Book31Controller(BOOK3Context context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string searchstring, decimal? low, decimal? high)
        {
            if (String.IsNullOrEmpty(searchstring)) { searchstring = ""; }
            if (low == null) { low = 0.0m; }
            if (high == null) { high = 999.0m; }
            var books = from m in _context.Book3
                        where m.Title.Contains(searchstring) && low.Value <= m.Price && m.Price <= high.Value
                        select m;
            return View(await books.ToListAsync());
        }

        public IActionResult infor()
        {
            List<Book3> books = _context.Book3.ToList();
            return View(books);
        }

        

        // GET: Book31/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book3 = await _context.Book3
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book3 == null)
            {
                return NotFound();
            }

            return View(book3);
        }

        // GET: Book31/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Book31/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Author,Publisher,ReleaseDate,Price")] Book3 book3)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book3);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book3);
        }

        // GET: Book31/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book3 = await _context.Book3.FindAsync(id);
            if (book3 == null)
            {
                return NotFound();
            }
            return View(book3);
        }

        // POST: Book31/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Author,Publisher,ReleaseDate,Price")] Book3 book3)
        {
            if (id != book3.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book3);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Book3Exists(book3.Id))
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
            return View(book3);
        }

        // GET: Book31/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book3 = await _context.Book3
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book3 == null)
            {
                return NotFound();
            }

            return View(book3);
        }

        // POST: Book31/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book3 = await _context.Book3.FindAsync(id);
            if (book3 != null)
            {
                _context.Book3.Remove(book3);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Book3Exists(int id)
        {
            return _context.Book3.Any(e => e.Id == id);
        }
    }
}
