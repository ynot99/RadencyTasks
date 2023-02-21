using HomeTask2.DataAccessLayer.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomeTask2.DataAccessLayer.Repository
{
    internal interface IHomeTask2Context
    {
        DbSet<Book> Books { get; set; }
        DbSet<Review> Reviews { get; set; }
        DbSet<Rating> Ratings { get; set; }
    }
}
