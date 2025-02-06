using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Library.Data;
using Library.Data.Models;
using Library.ViewModels.Ratings;
using Library.Services.Contracts;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Library.ViewModels.BookRatings;

namespace Library.Controllers
{
    [Authorize]
    public class BookRatingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IBookRatingsService bookRatingsService;

        public BookRatingsController(ApplicationDbContext context, IBookRatingsService bookRatingsService)
        {
            _context = context;
            this.bookRatingsService = bookRatingsService;
        }

        // GET: BookRatings
        [Authorize(Roles = GlobalConstants.AdminRole)]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Ratings.Include(b => b.Author).Include(b => b.User);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> UserIndex(IndexBookRatingsUserViewModel userBookRatings)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            userBookRatings = await bookRatingsService.GetUserBookRatingsAsync(userBookRatings, userId);

            return View(userBookRatings);
        }

        // GET: BookRatings/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookRating = await _context.Ratings
                .Include(b => b.Author)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookRating == null)
            {
                return NotFound();
            }

            return View(bookRating);
        }

        // GET: BookRatings/Create
        public IActionResult Create(string authorId)
        {
            CreateBookRatingViewModel model = new CreateBookRatingViewModel();
            model.AuthorId = authorId;
            return View(model);
        }

        // POST: BookRatings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBookRatingViewModel bookRating)
        {
            if (ModelState.IsValid)
            {
                var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                await bookRatingsService.CreateBookRatingAsync(bookRating, userId);
                return RedirectToAction(nameof(UserIndex));
            }
            return View(bookRating);
        }

        // GET: BookRatings/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookRating = await _context.Ratings.FindAsync(id);
            if (bookRating == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Id", bookRating.AuthorId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", bookRating.UserId);
            return View(bookRating);
        }

        // POST: BookRatings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,UserId,AuthorId,Rating,Review")] BookRating bookRating)
        {
            if (id != bookRating.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookRating);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookRatingExists(bookRating.Id))
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
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Id", bookRating.AuthorId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", bookRating.UserId);
            return View(bookRating);
        }

        // GET: BookRatings/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookRating = await _context.Ratings
                .Include(b => b.Author)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookRating == null)
            {
                return NotFound();
            }

            return View(bookRating);
        }

        // POST: BookRatings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var bookRating = await _context.Ratings.FindAsync(id);
            if (bookRating != null)
            {
                _context.Ratings.Remove(bookRating);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookRatingExists(string id)
        {
            return _context.Ratings.Any(e => e.Id == id);
        }
    }
}
