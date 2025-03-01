﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Library.Data;
using Library.Data.Models;
using Library.ViewModels.Books;
using Library.Services.Contracts;
using Library.Services;
using Library.ViewModels.Authors;

namespace Library.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IBooksService booksService;

        public BooksController(ApplicationDbContext context, IBooksService booksService)
        {
            _context = context;
            this.booksService = booksService;
        }

        // GET: Books
        public async Task<IActionResult> Index(IndexBooksViewModel books)
        {
            books = await booksService.GetBooksAsync(books);
            return View(books);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBookViewModel book)
        {
            if (ModelState.IsValid)
            {
               await booksService.CreateBookAsync(book);
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await booksService.GetBookToEditAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditBookViewModel editBook)
        {
            if (ModelState.IsValid)
            {
                await booksService.UpdateBookAsync(editBook);
                return RedirectToAction(nameof(Index));
            }
            return View(editBook);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(string id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
        public async Task<IActionResult> Seed()
        {
            IFormFile file = ConvertToIFormFile("C:\\Users\\HP\\Desktop\\Library\\Library\\Library\\wwwroot\\img\\defBook.jpg");
            for (int i = 0; i < 40; i++)
            {
                CreateBookViewModel model = new CreateBookViewModel()
                {
                    Title = $"Book {i}",
                    Description = $"Description for book {i}",
                    ImageFile = file
                };
                await booksService.CreateBookAsync(model);
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
