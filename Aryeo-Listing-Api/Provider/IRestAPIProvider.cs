using Aryeo_Listing_Api.Model;

namespace Aryeo_Listing_Api.Provider
{
    public interface IRestAPIProvider
    {
        public ResponseListing GetListingList(RequestListing input);
    }
}
