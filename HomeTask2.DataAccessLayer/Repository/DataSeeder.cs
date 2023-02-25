using HomeTask2.DataAccessLayer.Repository.Entities;

namespace HomeTask2.DataAccessLayer.Repository
{
    internal class DataSeeder
    {
        private readonly HomeTask2Context _context;

        public DataSeeder(HomeTask2Context context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Books.Any())
            {
                return;
            }
            List<Book> books = new();
            string[] authors = { "Jeremy", "Suzan", "Zack", "Cloud", "Apolo" };
            string[] genres = { "Comedy", "Horror", "Scifi", "Fantasy" };
            Random rand = new();
            for (int i = 0; i < 20; i++)
            {
                books.Add(new Book()
                {
                    Title = $"Book {i}",
                    Cover = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAQAAAAEACAIAAADTED8xAAADMElEQVR4nOzVwQnAIBQFQYXff81RUkQCOyDj1YOPnbXWPmeTRef+/3O/OyBjzh3CD95BfqICMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMK0CMO0TAAD//2Anhf4QtqobAAAAAElFTkSuQmCC",
                    Author = authors[rand.Next(authors.Length)],
                    Content = "Long time ago in a galaxy far far away...",
                    Genre = genres[rand.Next(genres.Length)]
                });
            }
            _context.Books.AddRange(books);

            List<Rating> ratings = new();
            int booksCount = books.Count;
            for (int i = 0; i < 200; i++)
            {
                ratings.Add(new Rating()
                {
                    Book = books[rand.Next(booksCount)],
                    Score = rand.Next(1, 5)
                });
            }
            _context.Ratings.AddRange(ratings);

            List<Review> reviews = new();
            for (int i = 0; i < 200; i++)
            {
                Book book = books[rand.Next(booksCount)];
                reviews.Add(new Review()
                {
                    Book = book,
                    Message = $"Book {book.Id} is {book.Genre}",
                    Reviewer = authors[rand.Next(authors.Length)]
                });
            }
            _context.Reviews.AddRange(reviews);

            _context.SaveChanges();
        }
    }
}
