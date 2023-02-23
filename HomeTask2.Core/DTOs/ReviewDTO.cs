namespace HomeTask2.Core.DTOs
{
    public class ReviewDTO
    {
        public long Id { get; set; }
        public string? Message { get; set; }
        public long BookId { get; set; }
        public string? Reviewer { get; set; }
    }
}
