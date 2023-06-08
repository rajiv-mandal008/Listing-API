using Aryeo_Listing_Api.Model;

namespace Aryeo_Listing_Api.Provider
{
    public interface IDataProvider
    {
        public void SaveListing(List<ListingDetails> list);
    }
}
