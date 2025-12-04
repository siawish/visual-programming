using Microsoft.AspNetCore.Mvc;
using BooksMVC.Data;
using BooksMVC.Models;

namespace BooksMVC.Controllers
{
    public class BooksController : Controller
    {
        private readonly AppDbContext _context;

        public BooksController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string searchString)
        {
            var books = from b in _context.Books
                        select b;

            if (!string.IsNullOrEmpty(searchString))
            {
                books = books.Where(b => b.Title.Contains(searchString) || 
                                        b.Author.Contains(searchString));
            }

            ViewData["CurrentFilter"] = searchString;
            return View(books.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                _context.SaveChanges();
                TempData["SuccessMessage"] = $"Book '{book.Title}' has been added successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();
            
            var book = _context.Books.Find(id);
            if (book == null) return NotFound();
            
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Book book)
        {
            if (id != book.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(book);
                _context.SaveChanges();
                TempData["SuccessMessage"] = $"Book '{book.Title}' has been updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var book = _context.Books.Find(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
                TempData["SuccessMessage"] = $"Book '{book.Title}' has been deleted successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Book not found!";
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();
            
            var book = _context.Books.Find(id);
            if (book == null) return NotFound();
            
            return View(book);
        }
    }
}
