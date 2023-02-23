using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task2.DAL.DbModels 
{
    public class Rating
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Book))]
        public int? BookId { get; set; } // 0 to many
        public virtual Book? Book { get; set; }

        [Range(0, 5, ErrorMessage = "Score must be from {0} to {1}")]
        public decimal Score { get; set; } = 0.0M;
    }
}
