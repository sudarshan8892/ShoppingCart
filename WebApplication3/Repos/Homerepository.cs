using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using WebApplication3.Models;

namespace WebApplication3.Repos
{
    public class Homerepository : IHomeRepository
    {
        private readonly Shopping_cartContext _context;
        public Homerepository(Shopping_cartContext shopping)
        {
            _context = shopping;
        }

        public List<Genre> GenresList()
        {
            var Geners = _context.Genres.ToList();
            return Geners;
        }
        public List<Book> getBooks(string search = "", int genereId = 0)
        {

            try
            {
                string serching = search?.ToLower();
                var book = (from bookList in _context.Books
                            join Genre in _context.Genres
                            on bookList.GenerId equals Genre.Id
                            where string.IsNullOrWhiteSpace(serching) || (bookList != null && bookList.BookName.ToLower().StartsWith(serching))
                            select new Book
                            {
                                Id = bookList.Id,
                                BookName = bookList.BookName,
                                GenerId = bookList.GenerId,
                                AuthorName = bookList.AuthorName,
                                Price = bookList.Price,
                                GenerName = Genre.GenerName,
                                BookImage = bookList.BookImage,

                            }).ToList();


                if (genereId > 0)
                {
                    book = (from b in _context.Books
                            join g in _context.Genres on b.GenerId equals g.Id
                            where b.GenerId == genereId
                            select
                            new Book
                            {
                                Id = b.Id,
                                BookName = b.BookName,
                                GenerId = b.GenerId,
                                AuthorName = b.AuthorName,
                                Price = b.Price,
                                GenerName = g.GenerName,
                                BookImage = b.BookImage,
                            }

                            ).ToList();
                }
                return book;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<User> GetuserDetails( string userName ,string Password )
        {
            var list = _context.Users.Where(u => u.PhoneNumber == userName && u.Password == Password).ToList();
            return list;
        }
    }
}

