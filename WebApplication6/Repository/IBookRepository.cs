using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication6.Models;

namespace WebApplication6.Repository
{
    public interface IBookRepository
    {
        Task<List<BookModel>> GetAllBooksAsync();

        Task<BookModel> GetBookByIdAsync(int id);

        Task<int> AddBookAsync(BookModel bookModel);

        Task UpdateBookAsync(int id, BookModel bookModel);

        Task UpdateBookPatch(int id, JsonPatchDocument bookModel);

        Task DeleteBookAsync(int id);

        Task<List<BookModel>> Search(string bookTitle);
    }
}
