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

        public async Task<bool> DeleteBookAsync(int id) {
            var book = await _dbManager.Books.FirstOrDefaultAsync(b => b.Id == id);
            if(book != null) {
                _dbManager.Remove(book);
                await _dbManager.SaveChangesAsync();
                return true;
            }
            else { return false; }
        }

        public async Task<List<Book>> GetAllBooksByValueAsync(string value) {
            return await _dbManager.Books.Where(b => b.Author == value || b.Title == value)
                .Include("Ratings")
                .Include("Reviews")
                .ToListAsync();
        }

        public async Task<Book?> GetBookByIdAsync(int id) {
            return await _dbManager.Books.Where(x => x.Id == id)
                .Include("Ratings")
                .Include("Reviews")
                .FirstOrDefaultAsync();
        }

        public async Task<List<Book>> GetTop10BooksByRatingAndReviewsAsync(string genre) {
            return await _dbManager.Books
                .Where(b => b.Ratings != null && b.Ratings.Count() > 10 && b.Genre.ToLower() == genre.ToLower())
                .Include("Ratings")
                .Include("Reviews")
                .OrderByDescending(b => b.Ratings.Sum(s => s.Score))
                .Take(10)
                .ToListAsync();
        }

        public async Task RateBook(int bookId, decimal score) {
            var book = await _dbManager.Books.FindAsync(bookId);
            if(book != null) {
                await _dbManager.Ratings.AddAsync(new Rating() { BookId = book.Id, Score = score });
                await _dbManager.SaveChangesAsync();
            }
        }

        public async Task<int> SaveBookAsync(Book book) {
            bool exist = await _dbManager.Books.AnyAsync(x => x.Id == book.Id);
            ValidationContext context = new ValidationContext(book);
            // Can be used in future as response, but task was another
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

        public async Task<int> SaveReviewAsync(int bookId, Review review) {
            var book = await _dbManager.Books.FindAsync(bookId);
            if(book != null) {
                review.BookId = book.Id;
                await _dbManager.Reviews.AddAsync(review);
                await _dbManager.SaveChangesAsync();
                return review.Id;
            }
            return -1;
        }
    }
}
