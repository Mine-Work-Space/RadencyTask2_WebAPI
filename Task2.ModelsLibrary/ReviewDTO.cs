using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.ModelsLibrary {
    public class ReviewDTO {
        public int Id { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Reviewer { get; set; } = string.Empty;
        public override string ToString() {
            return $"\n\t{{ ReviewDTO: {Id}, {Message}, {Reviewer} }}";
        }
    }
    public class ReviewWithoutIdDTO {
        public string Message { get; set; } = string.Empty;
        public string Reviewer { get; set; } = string.Empty;
        public override string ToString() {
            return $"\n\t{{ ReviewWithoutIdDTO: {Message}, {Reviewer} }}";
        }
    }
}
