using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Evaluation;
using Microsoft.EntityFrameworkCore;
using MyLibrary.Data;
using MyLibrary.Models;

namespace MyLibrary.Controllers
{
    public class BookSetsController : Controller
    {
        private readonly MyLibraryContext _context;

        public BookSetsController(MyLibraryContext context)
        {
            _context = context;
        }
         public async Task<IActionResult> Index()
        {
            return View(await _context.BookSet.ToListAsync());
        }
        public async Task<IActionResult> AddShelf(int bookSetId)
        {
            List<Book> booksInLibrary = await _context.Book
       .Where(b => b.LibraryId == bookSetId)
       .ToListAsync();

            if (booksInLibrary == null)
            {
                return NotFound();
            }
           
            //var maxHeight = booksInLibrary.Max(b => b.Height);
            //var totalWidth = booksInLibrary.Sum(b => b.Width);

            var shelves = await _context.Shelf
                .Where(s => s.LibraryId == booksInLibrary[0].LibraryId )
                .ToListAsync();

            ViewBag.ShelfId = new SelectList(shelves, "Id", "Id");

            return View(booksInLibrary);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public async Task<IActionResult> AddShelf(int id, int shelfId)
        {
            var bookSet = await _context.BookSet
                .Include(bs => bs.Books)
                .FirstOrDefaultAsync(bs => bs.Id == id);

            if (bookSet == null)
            {
                return NotFound();
            }

            var shelf = await _context.Shelf
                .FirstOrDefaultAsync(s => s.Id == shelfId);

            if (shelf == null || shelf.Space < bookSet.Books.Sum(b => b.Width))
            {
                return NotFound();
            }

            foreach (var book in bookSet.Books)
            {
                book.ShelfId = shelfId;
                shelf.Space -= book.Width;
            }

            _context.Update(shelf);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        // GET: BookSets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookSet = await _context.BookSet
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookSet == null)
            {
                return NotFound();
            }

            return View(bookSet);
        }

        // GET: BookSets/Create
        public IActionResult Create()
        {
            var viewModel = new BookSetViewModel();
            return View(viewModel);
        }

        // POST: BookSets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]

       
        public async Task<IActionResult> Create(BookSetViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                // יצירת אובייקט של BookSet מה-ViewModel
                var BookSetViewModel = new BookSet
                {
                    Name = viewModel.Name,
                   
                };

                // שמירת הסט
                _context.BookSet.Add(BookSetViewModel);
                await _context.SaveChangesAsync(); // שמירה לשם קבלת ה-Id של הסט

                // הגדרת מזהה הסט לספרים ושמירה
                foreach (var book in viewModel.Books)
                {
                    book.BookSetId = BookSetViewModel.Id;
                    _context.Book.Add(book);
                    _context.SaveChanges();
                }
                return RedirectToAction("AddShelf", new { bookSetId = BookSetViewModel.Id });

                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }




        // GET: BookSets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookSet = await _context.BookSet.FindAsync(id);
            if (bookSet == null)
            {
                return NotFound();
            }
            return View(bookSet);
        }

        // POST: BookSets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] BookSet bookSet)
        {
            if (id != bookSet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookSet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookSetExists(bookSet.Id))
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
            return View(bookSet);
        }

        // GET: BookSets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookSet = await _context.BookSet
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookSet == null)
            {
                return NotFound();
            }

            return View(bookSet);
        }

        // POST: BookSets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookSet = await _context.BookSet.FindAsync(id);
            if (bookSet != null)
            {
                _context.BookSet.Remove(bookSet);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookSetExists(int id)
        {
            return _context.BookSet.Any(e => e.Id == id);
        }
    }
}
