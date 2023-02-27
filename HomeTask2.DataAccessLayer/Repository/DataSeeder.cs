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
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed ac enim ut metus fringilla fringilla. Nulla facilisi. Fusce vitae mauris ut diam volutpat dapibus sed non nisl. In non lacus sed elit bibendum commodo. Aenean commodo tincidunt elit, sit amet tincidunt risus dignissim nec. Nullam vel orci tincidunt, sodales nisi in, malesuada arcu. Sed pharetra auctor sapien, vel consectetur leo aliquam nec. Duis nec mi dolor. Nam eu ultrices nulla. Nam fermentum mi sed nulla tincidunt efficitur. Maecenas aliquam dignissim dui, ut pharetra nisl bibendum in. Quisque imperdiet, nibh ac rhoncus ultrices, felis eros blandit nulla, vel tempor mauris massa ut purus. Nulla facilisi. Nam feugiat, magna sed interdum pulvinar, ex quam efficitur turpis, vitae tristique lacus ipsum vel metus.",
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
