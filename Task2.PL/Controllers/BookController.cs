using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations.Schema;
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
            if (String.IsNullOrWhiteSpace(order)) {
                _logMessage = "Input book title or author name.";
                Log("GetAllBooks", _logMessage + " Order string is empty.", false);
                return BadRequest(_logMessage);
            }
            if(order != "author" || order != "title") { // !!
                _logMessage = "Order != author && Order != title.";
                Log("GetAllBooks", _logMessage, false);
                return BadRequest(_logMessage);
            }
            else {
                var books = await _bookRepository.GetAllBooksByValueAsync(order);
                if(books.Count > 0) {
                    StringBuilder stringBuilder = new StringBuilder();
                    foreach (var book in books)
                        stringBuilder.AppendLine(book.ToString());
                    Log("GetAllBooks", stringBuilder.ToString(), true);

                    return Ok(books.Select(book => _mapper.Map<BookDTO>(book)));
                }
                else {
                    _logMessage = $"No books with order {order} founded.";
                    Log("GetAllBooks", _logMessage, false);
                    return BadRequest(_logMessage);
                }
            }
        }
        [HttpGet("recommended")]
        public async Task<ActionResult<IEnumerable<Book>>> GetTop10Books(string? genre) {
            if (String.IsNullOrWhiteSpace(genre)) {
                _logMessage = "Input genre of the books, please.";
                Log("GetTop10Books", _logMessage + " Genre string is empty.", false);
                return BadRequest(_logMessage);
            }
            else {
                var books = await _bookRepository.GetTop10BooksByRatingAndReviewsAsync(genre);
                if(books.Count > 0) {
                    StringBuilder stringBuilder = new StringBuilder();
                    var booksDTO = books.Select(book => _mapper.Map<BookDTO>(book));
                    foreach (var book in booksDTO)
                        stringBuilder.AppendLine(book.ToString());
                    Log("GetTop10Books", stringBuilder.ToString(), true);
                    return Ok(booksDTO);
                }
                else {
                    _logMessage = "Books was not found.";
                    Log("GetTop10Books", _logMessage, false);
                    return BadRequest(_logMessage);
                }
            }
        }
        [HttpGet("books/{id}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBookById(int id) {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if(book == null) {
                _logMessage = $"Book with id {id} was not found.";
                Log("GetBookById", _logMessage, false);
                return BadRequest(_logMessage);
            }
            else {
                var bookDTO = _mapper.Map<BookWithReviewsDTO>(book);
                Log("GetBookById", bookDTO.ToString(), true);
                return Ok(bookDTO);
            }
                
        }
        [HttpDelete("books/{id}")]
        public async Task<ActionResult> DeleteBook(int id, string secret = "C# in my heart") {
            if (secret != _configuration.GetValue<string>("SecretKey")) {
                _logMessage = "Secret key was wrong!";
                Log("DeleteBook", _logMessage, false);
                return BadRequest(_logMessage + " Try again.");
            }
            else {
                try {
                    bool success = await _bookRepository.DeleteBookAsync(id);
                    if (success) {
                        _logMessage = "Book was successfully deleted!";
                        Log("DeleteBook", _logMessage + $" Id was {id}.", true);
                        return Ok(_logMessage);
                    }
                    else {
                        _logMessage = "Current book was not found";
                        Log("DeleteBook", _logMessage + $" Id was {id}.", false);
                        return BadRequest(_logMessage);
                    }
                }
                catch (Exception ex) {
                    Log("DeleteBook", ex.Message, false);
                    return BadRequest(ex.Message);
                }
            }
        }
        [HttpPost("books/save")]
        public async Task<ActionResult> SaveBook(SaveBookDTO book) {
            if (book == null) {
                Log("SaveBook", "Input data is null." + book?.ToString(), false);
                return CreatedAtRoute("", new { id = -1 });
            }
            else {
                int bookId = await _bookRepository.SaveBookAsync(_mapper.Map<Book>(book));
                Log("SaveBook", "Successfully added: " + book.ToString(), true);
                return CreatedAtRoute("", new { id = bookId });
            }
        }
        [HttpPut("books/{id}/review")]
        public async Task<ActionResult> SaveReview(int id, [FromBody] ReviewWithoutIdDTO review) {
            int reviewId = await _bookRepository.SaveReviewAsync(id, _mapper.Map<Review>(review));
            if (reviewId == -1) {
                Log("SaveReview", "tSaving review return id -1!", false);
                return CreatedAtRoute("", new { id = -1 });
            }
            else {
                Log("SaveReview", "Saved: " + review.ToString(), true);
                return CreatedAtRoute("", new { id = reviewId });
            }
        }
        [HttpPut("books/{id}/rate")]
        public async Task<ActionResult> RateBook([FromRoute] int id, [FromBody] RatingDTO rating) {
            if (rating.Score < 1 || rating.Score > 5) {
                Log("RateBook", "Rate score < 1 or > 5.", false);
                return BadRequest($"Score must be between 1 and 5. Not {rating.Score}.");
            }
            else {
                bool success = await _bookRepository.RateBook(id, rating.Score);
                if (success) {
                    Log("RateBook", "Saved: " + rating.ToString(), true);
                    return Ok("Rate was successfully saved!");
                }
                else {
                    _logMessage = $"No book founded with id {id}";
                    Log("RateBook", _logMessage, false);
                    return BadRequest(_logMessage);
                }
            }
        }
        [NonAction]
        private void Log(string method, string message, bool isSuccess) {
            if(!isSuccess) {
                _logger.LogError("Method:" + method + "\n\t" + message);
            }
            else {
                _logger.LogInformation("Method:" + method + "\n\t" + message);
            }
        }
    }
}
