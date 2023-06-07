using Aryeo_Listing_Api.Model;

namespace Aryeo_Listing_Api.Provider
{
    public interface IListingProvider
    {
        public void GetListingList(RequestListing input);

        public void SaveListing(ListingDetails input);
    }
}
