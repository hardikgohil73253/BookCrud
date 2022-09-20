using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication6.Data;
using WebApplication6.Models;

namespace WebApplication6.Repository
{
    public class CartRepository:ICartRepository
    {
        private readonly BookStoreContext _context;

        public CartRepository(BookStoreContext context)
        {
            _context = context;
        }

        public async Task<int> AddBookToCart(CartModel cart)
        {
            var cartBook = new Cart()
            {
                BookId = cart.BookId,
                UserId = cart.UserId,
                NumberOfBook = 1,
            };

            _context.Cart.Add(cartBook);
            await _context.SaveChangesAsync();

            return cartBook.Id;
        }

        public async Task RemoveBookFromCart(int id)
        {
            var cart = new Cart() { Id = id };
            _context.Cart.Remove(cart);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBookNumber(int id, bool IsIncrement)
        {
            var cart = await _context.Cart.FindAsync(id);
            if (cart != null)
            {
                if (IsIncrement)
                {
                    cart.NumberOfBook = cart.NumberOfBook + 1;
                }
                else
                {
                    cart.NumberOfBook = cart.NumberOfBook - 1;
                }
                

                await _context.SaveChangesAsync();  
            }
        }

        public async Task<List<Cart>> GetBooksByUser(string userId)
        {
            List<Cart> books = new List<Cart>();

            books = await _context.Cart.Where(x => x.UserId == userId).ToListAsync();

            return books;
        }
    }
}
