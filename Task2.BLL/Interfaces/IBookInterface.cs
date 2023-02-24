using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.DAL.DbModels;

namespace Task2.BLL.Interfaces {
    internal interface IBookInterface {
        Task<List<Book>> GetAllBooksByValueAsync(string value);
        Task<List<Book>> GetTop10BooksByRatingAndReviewsAsync(string genre);
        Task<Book?> GetBookByIdAsync(int id);
        Task<bool> DeleteBookAsync(int id);
        Task<int> SaveBookAsync(Book book);
        Task<int> SaveReviewAsync(int bookId, Review review);
        Task<bool> RateBook(int bookId, decimal score);
    }
}
