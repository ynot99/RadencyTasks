using HomeTask2.DataAccessLayer.Repository;
using HomeTask2.DataAccessLayer.Repository.Entities;

namespace HomeTask2.DataAccessLayer
{
    public class BookDAL
    {
        private readonly HomeTask2Context _context;

        public BookDAL(HomeTask2Context context)
        {
            _context = context;
        }

        public IAsyncEnumerable<Book> GetAllBooks()
        {
            return _context.Books.AsAsyncEnumerable();
        }
    }
}
