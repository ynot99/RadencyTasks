using HomeTask2.Core.DTOs;
using HomeTask2.DataAccessLayer.Repository.Entities;

namespace HomeTask2.DataAccessLayer
{
    public interface IBookDAL
    {
        public Task<List<Book>> GetAll(string? order);
        public Task<Book> SaveOrModify(BookDTO book);
    }
}
