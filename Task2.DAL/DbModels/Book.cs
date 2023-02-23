using System.ComponentModel.DataAnnotations;

namespace Task2.DAL.DbModels
{
    public class Book
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(256, MinimumLength = 1, ErrorMessage = "Title must be minimum {1} characters and a maximum of {0} characters")]
        [DataType(DataType.Text)]
        public string Title { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Cover must have at least 1 character")]
        [DataType(DataType.Text)]
        public string Cover { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Content must have at least 1 character")]
        [DataType(DataType.Text)]
        public string Content { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Author must have at least 1 character")]
        [DataType(DataType.Text)]
        public string Author { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Genre must have at least 1 character")]
        [DataType(DataType.Text)]
        public string Genre { get; set; } = string.Empty;

        public virtual IEnumerable<Rating>? Ratings { get; set; }
        public virtual IEnumerable<Review>? Reviews { get; set; }
    }
}
