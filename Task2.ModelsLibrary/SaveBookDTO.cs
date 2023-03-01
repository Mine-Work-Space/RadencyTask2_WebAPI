using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.ModelsLibrary {
    public class SaveBookDTO {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Cover { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public override string ToString() {
            return $"{{ SaveBookDTO: Id '{Id}', Title '{Title}', Content '{Content}', Genre '{Genre}', Author '{Author}' }}";
        }
    }
}
