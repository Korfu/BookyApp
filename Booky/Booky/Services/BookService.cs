using System;
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

        public Book Get(Guid bookGuid)
        {
            return _bookRepo.Get(bookGuid);
        }

        public Guid Add(Book book)
        {
            return _bookRepo.Add(book);
        }

        public void Edit(Book book)
        {
            _bookRepo.Edit(book);
        }

        public void Delete(Guid bookGuid)
        {
            _bookRepo.Delete(bookGuid);
        }
    }
}
