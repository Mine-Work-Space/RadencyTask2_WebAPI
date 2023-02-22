using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task2.DAL.DbModels 
{
    public class Review
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(512, MinimumLength = 3, ErrorMessage = "Message must be minimum {1} characters and a maximum of {0} characters")]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; } = string.Empty;

        [ForeignKey(nameof(Book))]
        public int? BookId { get; set; } // 0 to many
        public virtual Book? Book { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Reviewer must have at least 1 character")]
        [DataType(DataType.Text)]
        public string Reviewer { get; set; } = string.Empty;
    }
}
