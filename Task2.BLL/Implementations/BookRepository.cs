using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.BLL.Interfaces;
using Task2.DAL;
using Task2.DAL.DbModels;

namespace Task2.BLL.Implementations {
    public class BookRepository : IBookInterface {
        private readonly ApiContext _dbManager = new ApiContext();

        public bool DeleteBook(int id) {
            var book = _dbManager.Books.FirstOrDefaultAsync(b => b.Id == id).Result;
            if(book != null) {
                _dbManager.Remove(book);
                _dbManager.SaveChangesAsync();
                return true;
            }
            else { return false; }
        }

        public Task<List<Book>> GetAllBooksByValueAsync(string value) {
            return _dbManager.Books.Where(b => b.Author == value || b.Title == value)
                .Include("Ratings")
                .Include("Reviews")
                .ToListAsync();
        }

        public Task<Book?> GetBookByIdAsync(int id) {
            return _dbManager.Books.Where(x => x.Id == id)
                .Include("Ratings")
                .Include("Reviews")
                .FirstOrDefaultAsync();
        }

        public async Task<List<Book>> GetTop10BooksByRatingAndReviewsAsync(string genre) {
            return await _dbManager.Books
                .Where(b => b.Ratings.Count() > 10 && b.Genre == genre)
                .Include("Ratings")
                .Include("Reviews")
                .OrderByDescending(b => b.Ratings.Sum(s => s.Score))
                .Take(10)
                .ToListAsync();
        }

        public async Task<int> SaveBookAsync(Book book) {
            bool exist = await _dbManager.Books.AnyAsync(x => x.Id == book.Id);
            ValidationContext context = new ValidationContext(book);
            List<ValidationResult> errors = new List<ValidationResult>();

            if(Validator.TryValidateObject(book, context, errors)) {
                if(!exist) {
                    await _dbManager.Books.AddAsync(book);
                }
                else {
                    _dbManager.Books.Update(book);
                }
                await _dbManager.SaveChangesAsync();
            }
            else {
                book.Id = -1;
            }
            return book.Id;
        }
    }
}
