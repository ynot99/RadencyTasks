using HomeTask2.DataAccessLayer.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomeTask2.DataAccessLayer.Repository
{
    public class HomeTask2Context : DbContext, IHomeTask2Context
    {
        public HomeTask2Context(DbContextOptions<HomeTask2Context> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Rating> Ratings { get; set; }
    }
}
