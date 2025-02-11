using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Library.Data;
using Library.Data.Models;
using Library.ViewModels.Authors;
using Library.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace Library.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuthorsService authorsService;

        public AuthorsController(ApplicationDbContext context, IAuthorsService authorsService)
        {
            _context = context;
            this.authorsService = authorsService;
        }

        // GET: Authors
        public async Task<IActionResult> Index(IndexAuthorsViewModel authors)
        {
            authors = await authorsService.GetAuthorsAsync(authors);
            return View(authors);
        }

        // GET: Authors/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Authors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // GET: Authors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = GlobalConstants.AdminRole)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAuthorViewModel author)
        {
            if (ModelState.IsValid)
            {
                await authorsService.CreateAuthorAsync(author);
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        // GET: Authors/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var author = await authorsService.GetAuthorToEditAsync(id);

            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditAuthorViewModel editAuthor)
        {
            if (ModelState.IsValid)
            {
                await authorsService.UpdateAuthorAsync(editAuthor);
                return RedirectToAction(nameof(Index));
            }
            return View(editAuthor);
        }

        // GET: Authors/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Authors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author != null)
            {
                _context.Authors.Remove(author);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorExists(string id)
        {
            return _context.Authors.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Seed()
        {
            IFormFile file = ConvertToIFormFile("C:\\Users\\HP\\Desktop\\Library\\Library\\Library\\wwwroot\\img\\avtordef.jpg");
            for (int i = 0; i < 40; i++)
            {
                CreateAuthorViewModel model = new CreateAuthorViewModel()
                {
                    Name = $"Author {i}",
                    Description = $"Description for author {i}",
                    ImageFile = file
                };
                await authorsService.CreateAuthorAsync(model);
            }
            return RedirectToAction(nameof(Index));
        }
        public IFormFile ConvertToIFormFile(string filePath)
        {
            var fileInfo = new FileInfo(filePath);
            var fileStream = new FileStream(filePath, FileMode.Open);

            IFormFile formFile = new FormFile(fileStream, 0, fileInfo.Length, fileInfo.Name, fileInfo.Name)
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/octet-stream"
            };

            return formFile;
        }
    }
}
