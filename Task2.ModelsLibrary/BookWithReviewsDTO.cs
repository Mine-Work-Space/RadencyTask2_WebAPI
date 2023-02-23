using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.ModelsLibrary {
    public class BookWithReviewsDTO {
        public int Id { get; set; } = -1;
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Cover { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public decimal Rating { get; set; } = 0.0M;
        public IEnumerable<ReviewDTO> Reviews { get; set; } = new List<ReviewDTO>();
        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            foreach (ReviewDTO review in Reviews) {
                sb.Append(review.ToString());
            }
            return $"{{ BookWithReviewsDTO: {Id}, {Title}, {Author}, {Cover}, {Content}, {Rating} \n" + sb.ToString() + "} ";
        }
    }
}
