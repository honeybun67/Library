using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Library.Data;
using Library.Data.Models;
using Library.ViewModels.ReccomendedBooks;
using Library.Services.Contracts;

namespace Library.Controllers
{
    public class RecommendedBooksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IRecommendedBookService recommendedBookService;

        public RecommendedBooksController(ApplicationDbContext context,IRecommendedBookService recommendedBookService)
        {
            _context = context;
            this.recommendedBookService = recommendedBookService;
        }

        // GET: RecommendedBooks
        public async Task<IActionResult> Index(IndexRecommendedBooksViewModel recommendedBooks)
        {
            recommendedBooks = await recommendedBookService.GetRecBooksAsync(recommendedBooks);
            return View(recommendedBooks);
        }

        // GET: RecommendedBooks/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recommendedBooks = await _context.RecommendedBooks
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
            return View();
        }

        // POST: RecommendedBooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateRecommendedBookViewModel recommendedBooks)
        {
            if (ModelState.IsValid)
            {
                await recommendedBookService.CreateRecommendedBookAsync(recommendedBooks);
                return RedirectToAction(nameof(Index));
            }
            return View(recommendedBooks);
        }

        // GET: RecommendedBooks/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recommendedBooks =await recommendedBookService.GetRecommendedBookToEditAsync(id);
            if (recommendedBooks == null)
            {
                return NotFound();
            }
            return View(recommendedBooks);
        }

        // POST: RecommendedBooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditRecommendedBookViewModel recommendedBookViewModel)
        {
           

            if (ModelState.IsValid)
            {
                await recommendedBookService.UpdateRecBookAsync(recommendedBookViewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(recommendedBookViewModel);
        }

        // GET: RecommendedBooks/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recommendedBooks = await _context.RecommendedBooks
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
