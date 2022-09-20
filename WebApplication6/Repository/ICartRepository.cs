using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication6.Data;
using WebApplication6.Models;

namespace WebApplication6.Repository
{
    public interface ICartRepository
    {
        Task<int> AddBookToCart(CartModel cart);

        Task RemoveBookFromCart(int id);

        Task<List<Cart>> GetBooksByUser(string userId);

        Task UpdateBookNumber(int id, bool IsIncrement);
    }
}
