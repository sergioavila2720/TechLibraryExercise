using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechLibrary.Data;
using TechLibrary.Domain;
using TechLibrary.Models;

namespace TechLibrary.Services
{
    public interface IBookService
    {
        // going to modify the service to be able to take in filters
        Task<List<Book>> GetBooksAsync(PageFilter pageFilter = null);
        Task<Book> GetBookByIdAsync(int bookid);
        Task<bool> CreateNewBookAsync(Book book);
        Task<bool> UpdateBookAsync(Book book);
    }

    public class BookService : IBookService
    {
        private readonly DataContext _dataContext;

        public BookService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Book>> GetBooksAsync(PageFilter pageFilter = null)
        {
            // if the page filter is not null return a list of the filtered pages, otherwise return a list containing everything 
            var queryable = _dataContext.Books.AsQueryable();
            
            if (pageFilter != null)
            {
                // Check if the search filter is empty and filter by it.
                if (!string.IsNullOrEmpty(pageFilter.SearchStr))
                {
                    queryable = _dataContext.Books
                                            .Where(t0 => t0.Title.ToLower().Contains(pageFilter.SearchStr.ToLower()) || 
                                                         t0.ShortDescr.ToLower().Contains(pageFilter.SearchStr.ToLower())).AsQueryable();
                }
                var skip = (pageFilter.PageNumber - 1) * pageFilter.PageSize;
                queryable = queryable.Skip(skip).Take(pageFilter.PageSize).AsQueryable();
                //queryable = _dataContext.Books.Skip(skip).Take(pageFilter.PageSize).AsQueryable();
            }
            

            return await queryable.ToListAsync();
        }

        public async Task<Book> GetBookByIdAsync(int bookid)
        {
            return await _dataContext.Books.SingleOrDefaultAsync(x => x.BookId == bookid);
        }
        // Method to Post a new book into the database
        public async Task<bool> CreateNewBookAsync(Book book)
        {
            _dataContext.Books.Add(book);
            await _dataContext.SaveChangesAsync();

            return true;

        }
        // Method to Update the book details
        public async Task<bool> UpdateBookAsync(Book book)
        {
            _dataContext.Entry(book).State = EntityState.Modified;

            try
            {
                await _dataContext.SaveChangesAsync();
                
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            return true;

        }
        
    }
}
