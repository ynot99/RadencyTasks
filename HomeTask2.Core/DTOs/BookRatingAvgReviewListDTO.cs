﻿namespace HomeTask2.Core.DTOs
{
    public class BookRatingAvgReviewListDTO
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Cover { get; set; }
        public string? Content { get; set; }
        public decimal Rating { get; set; }
        public List<ReviewForBookDTO>? Reviews { get; set; }
    }
}
