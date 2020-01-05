using System.Collections.Generic;
using Booky;

namespace BookyApi.Contracts
{
    public interface IBookService
    {
        IEnumerable<Book> GetAll();
    }
}
