namespace Aryeo_Listing_Api.Model
{
    public class RequestListing
    {
        public string? Address { get; set; }
        public string? List_Agent { get; set; }
        public string? Status { get; set; }
        public bool? Active { get; set; }
        public decimal? Price_Gte { get; set; }
        public decimal? Price_Lte { get; set; }
        public decimal? Square_Feet_Gte { get; set; }
        public decimal? Square_Feet_Lte { get; set; }
        public int? Bedrooms_Gte { get; set; }
        public int? Bedrooms_Lte { get; set; }
        public decimal? Bathrooms_Gte { get; set; }
        public decimal? Bathrooms_Lte { get; set; }
        public int? Per_Page { get; set; }
        public int? Page { get; set; }
    }
}
