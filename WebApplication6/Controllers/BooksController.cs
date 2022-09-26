using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WebApplication6.Models;
using WebApplication6.Repository;

namespace WebApplication6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository bookRepository;
        private readonly ILogger<BooksController> _logger;
        public BooksController(IBookRepository bookRepository, ILogger<BooksController> logger)
        {   
            this.bookRepository = bookRepository;
            _logger = logger;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllBooks()
        {
            //try
            //{
                _logger.LogInformation("BooksController.GetAllBooks method called!!!");
                var books = await bookRepository.GetAllBooksAsync();
            //throw new AccessViolationException("Violation Exception while accessing the resource.");
            throw new AccessViolationException("Violation Exception while accessing the resource.");
            return Ok(books);
            //}
            //catch(Exception e)
            //{
            //    _logger.LogInformation("something went wrong ${e}");
            //    return StatusCode(500, "Internal server error");
            //}
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            //try
            //{
                var book = await bookRepository.GetBookByIdAsync(id);
                if (book == null)
                    return NotFound();
                _logger.LogInformation("BookController.GetBookByID method call");
                return Ok(book);
            //}
            //catch(Exception e)
            //{
            //    _logger.LogInformation("something went wrong ${e}");
            //    return StatusCode(500, "Internal server error");
            //}
            
        }

        [HttpPost("")]
        public async Task<IActionResult> AddNewBook([FromBody]BookModel bookModel)
        {
            //try
            //{
                _logger.LogInformation("BookController.AddNewBook method call");
                var id = await bookRepository.AddBookAsync(bookModel);
                return CreatedAtAction(nameof(GetBookById), new { id = id, controller = "Books" }, id);
            //}
            //catch(Exception e)
            //{
            //    _logger.LogInformation("something went wrong ${e}");
            //    return StatusCode(500, "Internal server error");
            //}
            
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateBook([FromBody]BookModel bookModel, [FromRoute]int id)
        {
            //try
            //{
                _logger.LogInformation("BookController.Search method call");
                await bookRepository.UpdateBookAsync(id, bookModel);
                return Ok();
            //}
            //catch (Exception e)
            //{
            //    _logger.LogInformation("something went wrong ${e}");
            //    return StatusCode(500, "Internal server error");
            //}
            
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateBookPatch([FromBody]JsonPatchDocument bookModel, [FromRoute]int id)
        {
            await bookRepository.UpdateBookPatch(id, bookModel);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook([FromRoute]int id)
        {
            //try
            //{
                _logger.LogInformation("BookController.Search method call");
                await bookRepository.DeleteBookAsync(id);
                return Ok();
            //}
            //catch (Exception e)
            //{
            //    _logger.LogInformation("something went wrong ${e}");
            //    return StatusCode(500, "Internal server error");
            //}
            
        }

        [HttpGet("search/{title}")]
        public async Task<IActionResult> Search([FromRoute]string title)
        {
            //try
            //{
                _logger.LogInformation("BookController.Search method call");
                var books = await bookRepository.Search(title);
                return Ok(books);
            //}
            //catch (Exception e)
            //{
            //    _logger.LogInformation("something went wrong ${e}");
            //    return StatusCode(500, "Internal server error");
            //}
            
        }
    }
}
