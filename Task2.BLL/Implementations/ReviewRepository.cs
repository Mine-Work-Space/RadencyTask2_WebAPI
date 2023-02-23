using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.BLL.Interfaces;

namespace Task2.BLL.Implementations {
    public class ReviewRepository : IReviewInterface {
        public Task<int> SaveReview(int bookId, string message, string reviewer) {
            throw new NotImplementedException();
        }
    }
}
