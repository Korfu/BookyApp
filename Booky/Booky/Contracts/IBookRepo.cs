using System.Collections.Generic;
using Booky;

namespace BookyApi.Contracts
{
    public interface IBookRepo
    {
        IEnumerable<Book> GetAll();
    }
}
