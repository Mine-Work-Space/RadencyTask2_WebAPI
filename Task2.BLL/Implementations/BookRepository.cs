using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.BLL.Interfaces;
using Task2.DAL;
using Task2.DAL.DbModels;

namespace Task2.BLL.Implementations {
    public class BookRepository : IBookInterface {
        private readonly ApiContext _dbManager = new ApiContext();

        public void DeleteBook(int id, string secretKey) {
            var book = _dbManager.Books.FirstOrDefaultAsync(b => b.Id == id);
            if(book != null) {
                _dbManager.Remove(book);
            }
        }

        public Task<List<Book>> GetAllBooksByValue(string value) {
            return _dbManager.Books.Where(b => b.Author == value || b.Title == value)
                .Include("Ratings")
                .Include("Reviews")
                .ToListAsync();
        }

        public Task<Book?> GetBook(int id) {
            return _dbManager.Books.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<List<Book>> GetTop10BooksByRatingAndReviews(string genre) {
            return _dbManager.Books
                .Where(b => b.Ratings.Count() > 10)
                .Include("Ratings")
                .Include("Reviews")
                .OrderByDescending(b => b.Ratings.Sum(s => s.Score))
                .Take(10)
                .ToListAsync();
        }

        public async Task<int> SaveBook(Book book) {
            await _dbManager.Books.AddAsync(book);
            await _dbManager.SaveChangesAsync();
            return book.Id;
        }
    }
}
