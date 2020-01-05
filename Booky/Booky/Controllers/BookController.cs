using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookyApi.Contracts;
using BookyApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Booky.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private Book _book { get; set; } = new Book
        {
            Title = "Harry Potter and The Philosopher's Stone",
            Author = "J. K. Rowling",
            IsbnCode = "9788700631625",
            PublishedDate = new DateTime(1997, 6, 26),
            Summary = @"Harry Potter, an eleven-year-old orphan, discovers that 
                                he is a wizard and is invited to study at Hogwarts. 
                                Even as he escapes a dreary life and enters a world of magic, 
                                he finds trouble awaiting him."
        };

        private readonly ILogger<BookController> _logger;
        private readonly IBookService _bookService;

        public BookController(ILogger<BookController> logger, IBookService bookService)
        {
            _logger = logger;
            _bookService = bookService;
        }

        [HttpGet]
        [Route("GetAll")]
        public ActionResult<IEnumerable<Book>> GetAll()
        {
            var books = _bookService.GetAll();

            return Ok(books);
        }

        [HttpGet("{bookGuid}")]
        public ActionResult<Book> Get(Guid bookGuid)
        {
            var book = _bookService.Get(bookGuid);
            return Ok(book);
        }

        [HttpPost]
        public ActionResult Add(Book book)
        {
            var bookGuid = _bookService.Add(book);
            return Ok(bookGuid);
        }

        [HttpPut("{bookGuid}")]
        public ActionResult Edit(Book book)
        {
            _bookService.Edit(book);
            return Ok();
        }

        [HttpDelete]
        public ActionResult Delete()
        {
            return Ok();
        }
    }
}
