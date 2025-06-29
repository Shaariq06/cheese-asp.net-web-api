namespace MVCWebApplication.Models.DTO
{
    public class CheeseDTO
    {
        public required string Name { get; set; }
        public required string Type { get; set; }
        public required string Country { get; set; }
        public decimal PricePerKilo { get; set; }
        public int? StinkinessRating { get; set; }
        public bool? Aged { get; set; }
        public string? PairingSuggestion { get; set; }
    }
}