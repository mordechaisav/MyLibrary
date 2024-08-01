using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyLibrary.Data;
using MyLibrary.Models;

namespace MyLibrary.Controllers         
{
    public class BooksController : Controller
    {
        private readonly MyLibraryContext _context;

        public BooksController(MyLibraryContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var otzarContext = _context.Book.Include(b => b.BookSet).Include(b => b.Shelf);
            return View(await otzarContext.ToListAsync());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.BookSet)
                .Include(b => b.Shelf)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }
        [HttpGet]
        public async Task<IActionResult> AddShelf(int id)
        {
            var book = await _context.Book
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            var shelves = await _context.Shelf
                .Where(s => s.LibraryId == book.LibraryId && s.Space >=book.Width && s.Height >= book.Height)
                .ToListAsync();

            ViewBag.ShelfId = new SelectList(shelves, "Id", "Id");

            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddShelf(int id, int shelfId)
        {
            var book = await _context.Book
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            book.ShelfId = shelfId;
            var shelf = await _context.Shelf
                .FirstOrDefaultAsync(s => s.Id == shelfId);
            shelf.Space -= book.Width;

            _context.Update(book);
            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index)); 
        }




        // GET: Books/Create
        public IActionResult Create()
        {

            ViewData["LibraryId"] = new SelectList(_context.Library, "Id", "Genre");
            return View();
        }


        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Width,Height,LibraryId")] Book book)
        {
            ModelState.Remove("Library");
            ModelState.Remove("Shelf");
            ModelState.Remove("BookSet");
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(AddShelf), new { id = book.Id });

            }
            ViewData["LibraryId"] = new SelectList(_context.Library, "Id", "Id", book.LibraryId);
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            var shelves = await _context.Shelf
                .Where(s => s.LibraryId == book.LibraryId && s.Space >= book.Width && s.Height >= book.Height && s.Id != book.ShelfId)
                .ToListAsync();
            ViewData["BookSetId"] = new SelectList(_context.BookSet, "Id", "Id", book.BookSetId);
            ViewData["ShelfId"] = new SelectList(_context.Shelf, "Id", "Id", book.ShelfId);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Genre,Width,Height,BookSetId,ShelfId")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }
            var OBook = await _context.Book.FindAsync(book.Id);
            var OShelf = await _context.Shelf.FindAsync(OBook.ShelfId);
            var shelf = await _context.Shelf.FindAsync(book.ShelfId);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    OShelf.Space += book.Width;
                    shelf.Space -= book.Width;
                    _context.Update(OShelf);
                    _context.Update(shelf);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
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
            ViewData["BookSetId"] = new SelectList(_context.BookSet, "Id", "Id", book.BookSetId);
            ViewData["ShelfId"] = new SelectList(_context.Shelf, "Id", "Id", book.ShelfId);
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.BookSet)
                .Include(b => b.Shelf)
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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Book.FindAsync(id);
            if (book != null)
            {
                _context.Book.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.Id == id);
        }
    }
}
