using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.BLL.Interfaces {
    internal interface IRateInterface {
        Task RateBook(int bookId, int score);
    }
}
