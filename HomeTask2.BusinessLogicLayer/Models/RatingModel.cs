namespace HomeTask2.BusinessLogicLayer.Models
{
    public class RatingModel
    {
        public long Id { get; set; }
        public long BookId { get; set; }
        public decimal Score { get; set; }
    }
}
