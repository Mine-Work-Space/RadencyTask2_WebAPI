using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Task2.BLL.Implementations;
using Task2.DAL.DbModels;
using Task2.ModelsLibrary;

namespace Task2.PL.Controllers {
    [Route("api/")]
    [ApiController]
    public class BookController : ControllerBase {
        private readonly BookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private string _logMessage;
        public BookController(BookRepository bookRepository, IMapper mapper, IConfiguration config, ILogger<BookController> logger) {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _configuration = config;
            _logger = logger;
            _logMessage = String.Empty;
        }

        [HttpGet("books")]
        public async Task<ActionResult<IEnumerable<Book>>> GetAllBooks(string? order) {
            _logger.LogInformation("Method: GetAllBooks");
            if (String.IsNullOrWhiteSpace(order)) {
                _logMessage = "Input book title or author name.";
                _logger.LogError("\t" + _logMessage + " Order string is empty.");
                return BadRequest(_logMessage);
            }
            else {

                var books = await _bookRepository.GetAllBooksByValueAsync(order);
                _logger.LogInformation(books.ToString());
                // Sting Builder!!
                return Ok(books.Select(book => _mapper.Map<BookDTO>(book)));
            }
        }
        [HttpGet("recommended")]
        public async Task<ActionResult<IEnumerable<Book>>> GetTop10Books(string? genre) {
            _logger.LogInformation("Method: GetTop10Books");
            if (String.IsNullOrWhiteSpace(genre)) {
                _logMessage = "Input genre of the books, please.";
                _logger.LogError("\t" + _logMessage  + " Genre string is empty.");
                return BadRequest(_logMessage);
            }
            else {
                var books = await _bookRepository.GetTop10BooksByRatingAndReviewsAsync(genre);
                // Log in console
                StringBuilder stringBuilder = new StringBuilder();
                var booksDTO = books.Select(book => _mapper.Map<BookDTO>(book));
                foreach (var book in booksDTO)
                    stringBuilder.AppendLine(book.ToString());
                _logger.LogInformation("\t" + stringBuilder.ToString());

                return Ok(booksDTO);
            }
        }
        [HttpGet("books/{id}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBookById(int id) {
            _logger.LogInformation("Method: GetBookById");
            var book = await _bookRepository.GetBookByIdAsync(id);
            if(book == null) {
                _logMessage = $"Book with id {id} wasn't founded!";
                _logger.LogError("\t" + _logMessage);
                return BadRequest(_logMessage);
            }
            else {
                var bookDTO = _mapper.Map<BookWithReviewsDTO>(book);
                _logger.LogInformation("\t" + bookDTO.ToString());
                return Ok(bookDTO);
            }
                
        }
        [HttpDelete("books/{id}")]
        public async Task<ActionResult> DeleteBook(int id, string secret = "C# in my heart") {
            _logger.LogInformation("Method: DeleteBook");
            if (secret != _configuration.GetValue<string>("SecretKey")) {
                _logMessage = "Secret key was wrong!";
                _logger.LogError("\t" + _logMessage);
                return BadRequest(_logMessage + " Try again.");
            }
            else {
                try {
                    bool success = await _bookRepository.DeleteBookAsync(id);
                    if (success) {
                        _logMessage = "Book was successfully deleted!";
                        _logger.LogInformation($"\t{_logMessage} Id was {id}.");
                        return Ok(_logMessage);
                    }
                    else {
                        _logMessage = "Current book was not founded!";
                        _logger.LogError($"\t{_logMessage} Id was {id}.");
                        return BadRequest("Current book was not founded!");
                    }
                }
                catch (Exception ex) {
                    _logger.LogError("\t" + ex.Message);
                    return BadRequest(ex.Message);
                }
            }
        }
        [HttpPost("books/save")]
        public async Task<ActionResult> SaveBook(SaveBookDTO book) {
            _logger.LogInformation("Method: SaveBook");
            if (book == null) {
                _logger.LogError("\tInput data is null!");
                return CreatedAtRoute("", new { id = -1 });
            }
            else {
                int bookId = await _bookRepository.SaveBookAsync(_mapper.Map<Book>(book));
                _logger.LogInformation("\tSuccessfully added\n" + book.ToString());
                return CreatedAtRoute("", new { id = bookId });
            }
        }
        [HttpPut("books/{id}/review")]
        public async Task<ActionResult> SaveReview(int id, [FromBody] ReviewWithoutIdDTO review) {
            _logger.LogInformation("Method: SaveReview");
            int reviewId = await _bookRepository.SaveReviewAsync(id, _mapper.Map<Review>(review));
            if (reviewId == -1) {
                _logger.LogError("\tSaving review return id -1!");
                return CreatedAtRoute("", new { id = -1 });
            }
            else {
                _logger.LogInformation("\tSaved:" + review.ToString());
                return CreatedAtRoute("", new { id = reviewId });
            }
        }
        [HttpPut("books/{id}/rate")]
        public async Task<ActionResult> RateBook([FromRoute] int id, [FromBody] RatingDTO rating) {
            _logger.LogInformation("Method: RateBook");
            if (rating.Score < 1 || rating.Score > 5) {
                _logger.LogError("\tRate score < 1 or > 5.");
                return BadRequest($"Score must be between 1 and 5. Not {rating.Score}.");
            }
            else {
                await _bookRepository.RateBook(id, rating.Score);
                _logger.LogError("\tSaved:" + rating.ToString());
                return Ok("Rate was successfully saved!");
            }
        }
    }
}
