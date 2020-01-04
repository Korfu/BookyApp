using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Booky;
using BookyApi.Contracts;
using BookyApi.SqlExtensionMethods;
using Dapper;

namespace BookyApi.Repo
{
    public class BookRepo : IBookRepo
    {
        private SqlConnectionStringBuilder _builder { get; set; }
        public BookRepo()
        {
            _builder = new SqlConnectionStringBuilder();

            _builder.DataSource = "tcp:bookyserver.database.windows.net"; 
            _builder.UserID = "koko";            
            _builder.Password = "Spooky2020";     
            _builder.InitialCatalog = "BookyDb";
        }

        public IEnumerable<Book> GetAll()
        {
            using (var sqlConnection = new SqlConnection(_builder.ConnectionString))
            {
                var command = new SqlCommand(@"
                    SELECT Title, Author, IsbnCode, PublishedDate, Summary
                    FROM Books");

                var books =
                    sqlConnection.Query<Book>(command.CommandText, command.ToDynamicParameters());

                return books;
            }
        }
        
    }
}
