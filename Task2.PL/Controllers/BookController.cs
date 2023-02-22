using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Task2.BLL.Implementations;
using Task2.DAL.DbModels;
using Task2.ModelsLibrary;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Task2.PL.Controllers {
    [Route("api/")]
    [ApiController]
    public class BookController : ControllerBase {
        private readonly BookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookController(BookRepository bookRepository, IMapper mapper) {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        [HttpGet("books")]
        public ActionResult<IEnumerable<Book>> Get(string? order) {
            if (String.IsNullOrWhiteSpace(order))
                return BadRequest("Input book title or author name.");
            else {
                var books = _bookRepository.GetAllBooksByValue(order).Result;
                return Ok(books.Select(book => _mapper.Map<BookDTO>(book)));
            }
        }
        [HttpGet("recommended")]
        public ActionResult<IEnumerable<Book>> GetTop10Books(string? genre) {
            if (String.IsNullOrWhiteSpace(genre))
                return BadRequest("Input genre of the books, please.");
            else {
                var books = _bookRepository.GetTop10BooksByRatingAndReviews(genre).Result;
                return Ok(books.Select(book => _mapper.Map<BookDTO>(book)));
            }
        }
    }
}
