using System.Collections.Generic;
using Booky;
using BookyApi.Contracts;
using BookyApi.Repo;

namespace BookyApi.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepo _bookRepo;

        public BookService(IBookRepo bookRepo)
        {
            _bookRepo = bookRepo;
        }

        public IEnumerable<Book> GetAll()
        {
            return _bookRepo.GetAll();
        }
    }
}
