﻿namespace HomeTask1
{
    public class Payer
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public decimal Payment { get; set; }
        public DateOnly Date { get; set; }
        public long AccountNumber { get; set; }
        public string? Service { get; set; }
    }
}
