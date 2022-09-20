using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication6.Data;
using WebApplication6.Models;
using WebApplication6.Repository;

namespace WebApplication6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;
        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetCartBookByUser()
        {
            List<Cart> books = new List<Cart>();

            string userId = HttpContext.Session.GetString("UserId");

            books = await _cartRepository.GetBooksByUser(userId);
            return Ok(books);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddNewBookToCart([FromBody] CartModel Model)
        {
            var id = await _cartRepository.AddBookToCart(Model);
            return CreatedAtAction(nameof(GetCartBookByUser), new { id = id, controller = "Cart" }, id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookFromCart([FromRoute] int id)
        {
            await _cartRepository.RemoveBookFromCart(id);
            return Ok();
        }

        [HttpPost("updatebookcount/{id}/{IsIncrement}")]
        public async Task<IActionResult> UpdateBookCount([FromRoute] int id, [FromRoute]bool IsIncrement)
        {
            await _cartRepository.UpdateBookNumber(id, IsIncrement);
            return Ok();
        }
    }
}
