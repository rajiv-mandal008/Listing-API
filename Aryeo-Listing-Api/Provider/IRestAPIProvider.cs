using Aryeo_Listing_Api.Model;

namespace Aryeo_Listing_Api.Provider
{
    public interface IRestAPIProvider
    {
        public void GetListingList(RequestListing input);
    }
}
