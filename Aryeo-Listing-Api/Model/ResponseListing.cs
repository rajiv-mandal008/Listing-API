namespace Aryeo_Listing_Api.Model
{
    public class ResponseListing
    {
        public ResponseListing()
        {
            Data = new List<ListingDetails>();
        }
        public string Status { get; set; }
        public DateTime Timestamp { get; set; }
        public List<ListingDetails> Data { get; set; }
    }
}
