namespace HomeTask1.JSONModels
{
    internal class ServiceObj
    {
        public string? Name { get; set; }
        public List<PayerObj>? Payers { get; set; }
        public decimal Total { get; set; }
    }
}
