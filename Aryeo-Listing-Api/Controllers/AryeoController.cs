using Aryeo_Listing_Api.Model;
using Aryeo_Listing_Api.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aryeo_Listing_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AryeoController : ControllerBase
    {
        private readonly IRestAPIProvider _listingProvider;
        private readonly ILogger<AryeoController> _logger;

        public AryeoController(ILogger<AryeoController> logger, IRestAPIProvider listingProvider)
        {
            _logger = logger;
            _listingProvider = listingProvider;
        }

        [AllowAnonymous]
        [HttpGet(Name = "GetAreyoListing")]
        //[Route("GetAreyoListing/filter/{address?}/{agent?}/{status?}/{active?}/{price_gte?}/{price_lte?}/{square_feet_gte?}/{square_feet_lte?}/{bedrooms_gte?}/{bedrooms_lte?}/{bathrooms_gte}")]
        public IEnumerable<string> Get()
        {
            //string? address = null, string? agent = null, string? status = null, bool? active = null, decimal? price_gte = null,
            //decimal? price_lte = null, decimal? square_feet_gte = null, decimal? square_feet_lte = null, int? bedrooms_gte = null,
            //int? bedrooms_lte = null, decimal? bathrooms_gte = null, decimal? bathrooms_lte = null
            RequestListing RqListing = new();
            //{
            //    Address = address,
            //    List_Agent = agent,
            //    Status = status,
            //    Active = active,
            //    Price_Gte = price_gte,
            //    Price_Lte = price_lte,
            //    Square_Feet_Gte = square_feet_gte,
            //    Square_Feet_Lte = square_feet_lte,
            //    Bedrooms_Gte = bedrooms_gte,
            //    Bedrooms_Lte = bedrooms_lte,
            //    Bathrooms_Gte = bathrooms_gte,
            //    Bathrooms_Lte = bathrooms_lte,
            //    Per_Page = 25,
            //    Page = 1
            //};
            _listingProvider.GetListingList(RqListing);
            yield return "";
        }
    }
}