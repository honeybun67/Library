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
using Library.ViewModels.AuthorRatings;

namespace Library.Controllers
{
    [Authorize]
    public class AuthorRatingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuthorRatingsService AuthorRatingsService;

        public AuthorRatingsController(ApplicationDbContext context, IAuthorRatingsService AuthorRatingsService)
        {
            _context = context;
            this.AuthorRatingsService = AuthorRatingsService;
        }

        // GET: AuthorRatings
        [Authorize(Roles = GlobalConstants.AdminRole)]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Ratings.Include(b => b.Author).Include(b => b.User);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> UserIndex(IndexAuthorRatingsUserViewModel userAuthorRatings)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            userAuthorRatings = await AuthorRatingsService.GetUserAuthorRatingsAsync(userAuthorRatings, userId);

            return View(userAuthorRatings);
        }

        // GET: AuthorRatings/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var AuthorRating = await _context.Ratings
                .Include(b => b.Author)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (AuthorRating == null)
            {
                return NotFound();
            }

            return View(AuthorRating);
        }

        // GET: AuthorRatings/Create
        public IActionResult Create(string authorId)
        {
            CreateAuthorRatingViewModel model = new CreateAuthorRatingViewModel();
            model.AuthorId = authorId;
            return View(model);
        }

        // POST: AuthorRatings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAuthorRatingViewModel AuthorRating)
        {
            if (ModelState.IsValid)
            {
                var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                await AuthorRatingsService.CreateAuthorRatingAsync(AuthorRating, userId);
                return RedirectToAction(nameof(UserIndex));
            }
            return View(AuthorRating);
        }

        // GET: AuthorRatings/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var AuthorRating = await _context.Ratings.FindAsync(id);
            if (AuthorRating == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Id", AuthorRating.AuthorId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", AuthorRating.UserId);
            return View(AuthorRating);
        }

        // POST: AuthorRatings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,UserId,AuthorId,Rating,Review")] AuthorRating AuthorRating)
        {
            if (id != AuthorRating.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(AuthorRating);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorRatingExists(AuthorRating.Id))
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
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Id", AuthorRating.AuthorId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", AuthorRating.UserId);
            return View(AuthorRating);
        }

        // GET: AuthorRatings/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var AuthorRating = await _context.Ratings
                .Include(b => b.Author)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (AuthorRating == null)
            {
                return NotFound();
            }

            return View(AuthorRating);
        }

        // POST: AuthorRatings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var AuthorRating = await _context.Ratings.FindAsync(id);
            if (AuthorRating != null)
            {
                _context.Ratings.Remove(AuthorRating);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorRatingExists(string id)
        {
            return _context.Ratings.Any(e => e.Id == id);
        }
        public async Task<IActionResult> Seed()
        {
            await AuthorRatingsService.SeedReviewsAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
