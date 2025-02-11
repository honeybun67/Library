using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Library.Data;
using Library.Data.Models;

namespace Library.Controllers
{
    public class RecommendedBooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RecommendedBooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RecommendedBooks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RecommendedBooks.Include(r => r.Book);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RecommendedBooks/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recommendedBooks = await _context.RecommendedBooks
                .Include(r => r.Book)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recommendedBooks == null)
            {
                return NotFound();
            }

            return View(recommendedBooks);
        }

        // GET: RecommendedBooks/Create
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id");
            return View();
        }

        // POST: RecommendedBooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BookId")] RecommendedBooks recommendedBooks)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recommendedBooks);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id", recommendedBooks.BookId);
            return View(recommendedBooks);
        }

        // GET: RecommendedBooks/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recommendedBooks = await _context.RecommendedBooks.FindAsync(id);
            if (recommendedBooks == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id", recommendedBooks.BookId);
            return View(recommendedBooks);
        }

        // POST: RecommendedBooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,BookId")] RecommendedBooks recommendedBooks)
        {
            if (id != recommendedBooks.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recommendedBooks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecommendedBooksExists(recommendedBooks.Id))
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
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id", recommendedBooks.BookId);
            return View(recommendedBooks);
        }

        // GET: RecommendedBooks/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recommendedBooks = await _context.RecommendedBooks
                .Include(r => r.Book)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recommendedBooks == null)
            {
                return NotFound();
            }

            return View(recommendedBooks);
        }

        // POST: RecommendedBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var recommendedBooks = await _context.RecommendedBooks.FindAsync(id);
            if (recommendedBooks != null)
            {
                _context.RecommendedBooks.Remove(recommendedBooks);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecommendedBooksExists(string id)
        {
            return _context.RecommendedBooks.Any(e => e.Id == id);
        }
    }
}
