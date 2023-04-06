namespace HomeTask2.DataAccessLayer.Repository.Entities
{
    public class Rating
    {
        public long Id { get; set; }
        public long BookId { get; set; }
        public decimal Score { get; set; }

        public virtual Book? Book { get; set; }
    }
}
