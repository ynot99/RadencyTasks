namespace HomeTask1.JSONModels
{
    internal class PayerObj
    {
        public string? Name { get; set; }
        public decimal Payment { get; set; }
        // TODO change to DateOnly
        public DateTime Date { get; set; }
        public long AccountNumber { get; set; }
    }
}
