using System.ComponentModel.DataAnnotations.Schema;

namespace Task2.ModelsLibrary {
    public class BookDTO {
        public int Id { get; set; } = -1;
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public decimal Rating { get; set; } = 0.0M;
        public int ReviewsNumber { get; set; } = 0;
        public override string ToString() {
            return $"\n{{ {Id}, {Title}, {Author}, {Rating}, {ReviewsNumber} }}";
        }
    }
}