namespace HomeTask2.BusinessLogicLayer.Models
{
    public class ReviewModel
    {
        public long Id { get; set; }
        public string? Message { get; set; }
        public long BookId { get; set; }
        public string? Reviewer { get; set; }
    }
}
