using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.DAL.DbModels;

namespace Task2.BLL.Interfaces {
    internal interface IBookInterface {
        Task<List<Book>> GetAllBooksByValue(string value);
        Task<List<Book>> GetTop10BooksByRatingAndReviews(string genre);
        Task<Book?> GetBook(int id);
        void DeleteBook(int id, string secretKey);
        Task<int> SaveBook(Book book);
    }
}
