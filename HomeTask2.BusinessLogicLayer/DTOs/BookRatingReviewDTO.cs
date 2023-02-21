namespace HomeTask2.BusinessLogicLayer.DTOs
{
    public class BookRatingReviewDTO
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public decimal Rating { get; set; }
        public long ReviewsNumber { get; set; }
    }
}
