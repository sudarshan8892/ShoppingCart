using WebApplication3.Models;

namespace WebApplication3
{
    public interface IHomeRepository
    {
        List<Book> getBooks(string search = "", int genereId = 0);
        List<Genre> GenresList();
        List<User> GetuserDetails(string userName, string Password);
    }
}