namespace Aryeo_Listing_Api.Model
{
    public class ResponseListing
    {
        public ResponseListing()
        {
            ListingDetails = new List<ListingDetails>();
        }
       
        public List<ListingDetails> ListingDetails { get; set; }
    }
}
