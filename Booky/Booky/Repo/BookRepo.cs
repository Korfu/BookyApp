using System;
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
            _builder = new SqlConnectionStringBuilder
            {
                DataSource = "tcp:bookyserver.database.windows.net",
                UserID = "koko",
                Password = "Spooky2020",
                InitialCatalog = "BookyDb"
            };
        }

        public IEnumerable<Book> GetAll()
        {
            using var sqlConnection = new SqlConnection(_builder.ConnectionString);
            var command = new SqlCommand(@"
                    SELECT Title, Author, IsbnCode, PublishedDate, Summary
                    FROM Books");

            var books =
                sqlConnection.Query<Book>(command.CommandText, command.ToDynamicParameters());

            return books;
        }
        public Book Get(Guid bookGuid)
        {
            using var sqlConnection = new SqlConnection(_builder.ConnectionString);
            var command = new SqlCommand(@"
                    SELECT Title, Author, IsbnCode, PublishedDate, Summary
                    FROM Books
                    WHERE Id = @BookGuid");
            
            command.Parameters.AddWithValue("BookGuid", bookGuid);
            return sqlConnection.QuerySingleOrDefault<Book>(command.CommandText, command.ToDynamicParameters());
        }

        public Guid Add(Book book)
        {
            using var sqlConnection = new SqlConnection(_builder.ConnectionString);
            var command = new SqlCommand(@"
                    INSERT INTO Books 
                        (Title, Author, IsbnCode, PublishedDate, Summary)
                    OUTPUT INSERTED.Id
                    VALUES 
                        (@Title, @Author, @IsbnCode, @PublishedDate, @Summary)
                    ");

            command.Parameters.AddWithValue("Title", book.Title);
            command.Parameters.AddWithValue("Author", book.Author);
            command.Parameters.AddWithValue("IsbnCode", book.IsbnCode);
            command.Parameters.AddWithValue("PublishedDate", book.PublishedDate);
            command.Parameters.AddWithValue("Summary", book.Summary);

            return sqlConnection.QuerySingle<Guid>(command.CommandText, command.ToDynamicParameters());
        }

        public void Edit(Book book)
        {
            using var sqlConnection = new SqlConnection(_builder.ConnectionString);
            var command = new SqlCommand(@"
                    UPDATE Books
                    SET Title = @Title, 
                        Author = @Author, 
                        IsbnCode = @IsbnCode, 
                        PublishedDate = @PublishedDate,
                        Summary = @Summary
                    WHERE Id = @Guid
                    ");

            command.Parameters.AddWithValue("Guid", book.Guid);
            command.Parameters.AddWithValue("Title", book.Title);
            command.Parameters.AddWithValue("Author", book.Author);
            command.Parameters.AddWithValue("IsbnCode", book.IsbnCode);
            command.Parameters.AddWithValue("PublishedDate", book.PublishedDate);
            command.Parameters.AddWithValue("Summary", book.Summary);

            sqlConnection.Execute(command.CommandText, command.ToDynamicParameters());
        }

        public void Delete(Guid bookGuid)
        {
            using var sqlConnection = new SqlConnection(_builder.ConnectionString);
            var command = new SqlCommand(@"
                    DELETE FROM Books
                    WHERE Id = @Guid
                    ");

            command.Parameters.AddWithValue("Guid", bookGuid);

            sqlConnection.Execute(command.CommandText, command.ToDynamicParameters());
        }
    }
}
