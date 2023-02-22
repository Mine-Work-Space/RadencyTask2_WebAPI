using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task2.DAL.DbModels 
{
    public class Rating
    {
        public Guid Id { get; set; }

        [ForeignKey(nameof(Book))]
        public Guid? BookId { get; set; } // 0 to many
        public virtual Book? Book { get; set; }

        [Range(0, 5, ErrorMessage = "Score must be from {0} to {1}")]
        public float Score { get; set; } = 0.0f;
    }
}
