namespace HomeTask2.DataAccessLayer.Repository.Entities
{
    public class Review
    {
        public long Id { get; set; }
        public string? Message { get; set; }
        public long BookId { get; set; }
        public string? Reviewer { get; set; }
    }
}
