using Dapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebApplication6.Data;
using WebApplication6.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebApplication6.Repository
{
    public class BookRepository:IBookRepository
    {
        private readonly BookStoreContext _context;

        public BookRepository(BookStoreContext context)
        {
            _context = context;
        }

        //public async Task<List<BookModel>> GetAllBooksAsync()
        //{
        //    var records = await _context.Books.Select(x => new BookModel()
        //    {
        //        Id = x.Id,
        //        Title = x.Title,
        //        Description = x.Description
        //    }).ToListAsync();

        //    return records;
        //}

        public async Task<List<BookModel>> GetAllBooksAsync()
        {
            //var query = "SELECT * FROM Books ";
            //using (var connection = _context.CreateConnection())
            //{
            //    var company = await connection.QueryAsync<BookModel>(query);
            //    return (List<BookModel>)company;
            //}

            var procedure = "BookGet";
            using (var connection = _context.CreateConnection())
            {
                var books = await connection.QueryAsync<BookModel>(procedure, null, commandType: CommandType.StoredProcedure);
                return books.ToList();
            }
        }

        public async Task<BookModel> GetBookByIdAsync(int id)
        {
            var procedureName = "BookbyBookId";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var book = await connection.QueryFirstOrDefaultAsync<BookModel>
                    (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return book;
            }
        }

        //public async Task<BookModel> GetBookByIdAsync(int id)
        //{
        //    var records = await _context.Books.Where(x=>x.Id == id).Select(x => new BookModel()
        //    {
        //        Id = x.Id,
        //        Title = x.Title,
        //        Description = x.Description
        //    }).FirstOrDefaultAsync();

        //    return records;
        //}

        public async Task<int> AddBookAsync(BookModel bookModel)
        {
            var book = new Books()
            {
                Title = bookModel.Title,
                Description = bookModel.Description
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book.Id;
        }

        public async Task UpdateBookAsync(int id, BookModel bookModel)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                book.Title = bookModel.Title;
                book.Description = bookModel.Description;

                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateBookPatch(int id, JsonPatchDocument bookModel)
        {
            var book = await _context.Books.FindAsync(id);
            if(book != null)
            {
                bookModel.ApplyTo(book);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = new Books() { Id = id };
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

        public async Task<List<BookModel>> Search(string bookTitle)
        {
            var records = await _context.Books.Where(x=>x.Title == bookTitle).Select(x => new BookModel()
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description
            }).ToListAsync();

            return records;
        }
    }
}
