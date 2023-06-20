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
        public IEnumerable<ResponseListing> Get()
        {
            _logger.LogInformation("GetAreyoListing method started!");
            RequestListing RqListing = new();
            var response = _listingProvider.GetListingList(RqListing);
            yield return response;
        }
    }
}