namespace HomeTask2.Core.DTOs
{
    public class RatingDTO
    {
        public long Id { get; set; }
        public long BookId { get; set; }
        public decimal Score { get; set; }
    }
}
