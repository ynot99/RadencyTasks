using HomeTask2.DataAccessLayer.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomeTask2.DataAccessLayer.Repository
{
    internal interface IHomeTask2Context
    {
        internal DbSet<Book> Books { get; set; }
        internal DbSet<Review> Reviews { get; set; }
        internal DbSet<Rating> Ratings { get; set; }
    }
}
