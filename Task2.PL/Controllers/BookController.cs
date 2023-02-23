using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
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
        private readonly IConfiguration _configuration;
        public BookController(BookRepository bookRepository, IMapper mapper, IConfiguration config) {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _configuration = config;
        }

        [HttpGet("books")]
        public ActionResult<IEnumerable<Book>> Get(string? order) {
            if (String.IsNullOrWhiteSpace(order))
                return BadRequest("Input book title or author name.");
            else {
                var books = _bookRepository.GetAllBooksByValueAsync(order).Result;
                return Ok(books.Select(book => _mapper.Map<BookDTO>(book)));
            }
        }
        [HttpGet("recommended")]
        public ActionResult<IEnumerable<Book>> GetTop10Books(string? genre) {
            if (String.IsNullOrWhiteSpace(genre))
                return BadRequest("Input genre of the books, please.");
            else {
                var books = _bookRepository.GetTop10BooksByRatingAndReviewsAsync(genre).Result;
                return Ok(books.Select(book => _mapper.Map<BookDTO>(book)));
            }
        }
        [HttpGet("books/{id}")]
        public ActionResult<IEnumerable<Book>> GetBookById(int id) {
            var book = _bookRepository.GetBookByIdAsync(id).Result;
            return Ok(_mapper.Map<BookWithReviewsDTO>(book));
        }
        [HttpDelete("books/{id}")]
        public ActionResult DeleteBook(int id, string secret = "C# in my heart") {
            if (secret != _configuration.GetValue<string>("SecretKey"))
                return BadRequest("Sorry, secret key was wrong!");
            else {
                try {
                    bool success = _bookRepository.DeleteBook(id);
                    if (success)
                        return Ok("Book was successfully deleted!");
                    else
                        return BadRequest("Current book was not founded!");
                }
                catch(Exception ex) {
                    return BadRequest(ex.Message);
                }
            }
        }
        [HttpPost("books/save")]
        public async Task<ActionResult> SaveBook(SaveBookDTO book) {
            if (book == null) return CreatedAtRoute("", new {id = -1});
            else {
                int bookId = await _bookRepository.SaveBookAsync(_mapper.Map<Book>(book));
                return CreatedAtRoute("", new { id = bookId });
            }
        }
    }
}
