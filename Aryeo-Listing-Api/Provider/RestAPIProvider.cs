using Aryeo_Listing_Api.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Aryeo_Listing_Api.Provider
{
    public class RestAPIProvider : IRestAPIProvider
    {
        protected readonly IConfiguration _configuration;
        private readonly IDataProvider _dataProvider;
        private readonly ILogger<RestAPIProvider> _logger;
        public RestAPIProvider(IConfiguration configuration, IDataProvider dataProvider, ILogger<RestAPIProvider> logger)
        {
            _configuration = configuration;
            _dataProvider = dataProvider;
            _logger = logger;
        }
        public ResponseListing GetListingList(RequestListing input)
        {
            string url = _configuration.GetValue<string>("Baseurl");
            string token = _configuration.GetValue<string>("BearerToken");
            string? response = RetriveDataFromRestAPI(url, token);

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore,
            };
            ResponseListing responseObj = new();
            if (response != null)
            {
                var jsonModel = JsonConvert.DeserializeObject<JObject>(response, settings);
                try
                {
                    if (jsonModel["status"].ToString() == "success")
                    {
                        JObject jsonMeta = (JObject)jsonModel["meta"];
                        int totalPage = jsonMeta.GetValue("last_page") != null ? Convert.ToInt32(jsonMeta.GetValue("last_page")) : 0;
                        int totalCount = jsonMeta.GetValue("total") != null ? Convert.ToInt32(jsonMeta.GetValue("total")) : 0;
                        //Process first time call data
                        ProccessJsonData(jsonModel);
                        for (int i = 2; i < totalPage; i++)
                        {
                            var newurl = url + "& page=" + i;
                            string childResponse = RetriveDataFromRestAPI(newurl, token);

                            var childJsonModel = JsonConvert.DeserializeObject<JObject>(childResponse, settings);

                            //Process 2nd onward call data
                            ProccessJsonData(childJsonModel);
                        }

                        responseObj.ResponseMessage = "Record saved successfuly!";
                        responseObj.TotalRecordInserted = totalCount;
                    }
                    else
                    {
                        responseObj.ResponseMessage = "We are not getting any new listing from Aryeo API!,So no new record saved.";
                        responseObj.TotalRecordInserted = 0;
                    }

                    _logger.LogInformation("GetListingList method completed.");
                    return responseObj;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw;
                }
            }
            else
            {
                responseObj.ResponseMessage = "We are getting null response from Aryeo API!";
                responseObj.TotalRecordInserted = 0;

                _logger.LogInformation("GetListingList method completed.");
                return responseObj;
            }
        }

        private void ProccessJsonData(JObject jsonModel)
        {
            List<ListingDetails> list = new();

            if (jsonModel["data"] != null)
            {
                foreach (JObject item in jsonModel["data"])
                {
                    if (item != null)
                    {
                        AddressDetails address = new();
                        JObject? jsonAddress = item["address"] != null ? (JObject)item["address"] : null;
                        if (jsonAddress == null) continue;

                        address.Id = Convert.ToString(jsonAddress.GetValue("id"));
                        address.Latitude = jsonAddress.GetValue("latitude") != null ? Convert.ToDecimal(jsonAddress.GetValue("latitude")) : 0;
                        address.Longitude = jsonAddress.GetValue("longitude") != null ? Convert.ToDecimal(jsonAddress.GetValue("longitude")) : 0;
                        address.Street_Number = Convert.ToString(jsonAddress.GetValue("street_number"));
                        address.Street_Name = Convert.ToString(jsonAddress.GetValue("street_name"));
                        address.Unit_Number = Convert.ToString(jsonAddress.GetValue("unit_number"));
                        address.Postal_Code = Convert.ToString(jsonAddress.GetValue("postal_code"));
                        address.City = Convert.ToString(jsonAddress.GetValue("city"));
                        address.City_Region = Convert.ToString(jsonAddress.GetValue("city_region"));
                        address.County_Or_Parish = Convert.ToString(jsonAddress.GetValue("county_or_parish"));
                        address.State_Or_Province = Convert.ToString(jsonAddress.GetValue("state_or_province"));
                        address.State_Or_Province_Region = Convert.ToString(jsonAddress.GetValue("state_or_province_region"));
                        address.Country = Convert.ToString(jsonAddress.GetValue("country"));
                        address.Country_Region = Convert.ToString(jsonAddress.GetValue("country_region"));
                        address.Timezone = Convert.ToString(jsonAddress.GetValue("timezone"));
                        address.Unparsed_Address = Convert.ToString(jsonAddress.GetValue("unparsed_address"));
                        address.IS_Map_Dirty = jsonAddress.GetValue("is_map_dirty") != null ? Convert.ToBoolean(jsonAddress.GetValue("is_map_dirty")) : false;

                        LotDetails lot = new();
                        JObject? jsonLot = item["lot"] != null ? (JObject)item["lot"] : null;
                        if (jsonLot != null)
                        {
                            lot.Lot_Size_Acres = Convert.ToString(jsonLot.GetValue("lot_size_acres"));
                            lot.Lot_Open_Parking_Spaces = Convert.ToString(jsonLot.GetValue("lot_open_parking_spaces"));
                        }

                        BuildingDetails build = new();
                        JObject? jsonBuilding = item["building"] != null ? (JObject)item["building"] : null;
                        if (jsonBuilding != null)
                        {
                            build.Bedrooms = Convert.ToString(jsonBuilding.GetValue("bedrooms"));
                            build.Bathrooms = Convert.ToString(jsonBuilding.GetValue("bathrooms"));
                            build.Square_Feet = Convert.ToString(jsonBuilding.GetValue("square_feet"));
                            build.Year_Built = Convert.ToString(jsonBuilding.GetValue("year_built"));
                        }
                        JObject? jsonPrice = item["price"] != null ? (JObject)item["price"] : null;
                        var price = string.Empty;
                        if (jsonPrice != null)
                        {
                            price = Convert.ToString(jsonPrice.GetValue("list_price"));
                        }

                        ListingDetails data = new()
                        {
                            Address = address,
                            Id = Convert.ToString(item.GetValue("id")),
                            AddressId = address.Id,
                            Mls_Number = Convert.ToString(item.GetValue("mls_number")),
                            Type = Convert.ToString(item.GetValue("type")),
                            Sub_Type = Convert.ToString(item.GetValue("sub_type")),
                            Status = Convert.ToString(item.GetValue("status")),
                            Standard_Status = Convert.ToString(item.GetValue("standard_status")),
                            Thumbnail_URL = Convert.ToString(item.GetValue("thumbnail_url")),
                            Large_Thumbnail_URL = Convert.ToString(item.GetValue("large_thumbnail_url")),
                            Description = Convert.ToString(item.GetValue("description")),
                            List_Price = price,
                            LotDetails = lot,
                            Building = build,
                            Floor_Plans = Convert.ToString(item.GetValue("floor_plans")),
                            Interactive_Content = Convert.ToString(item.GetValue("interactive_content")),
                            Downloads_Enabled = jsonAddress.GetValue("downloads_enabled") != null ? Convert.ToBoolean(item.GetValue("downloads_enabled")) : false
                        };

                        list.Add(data);
                    }
                }
            }
            _dataProvider.SaveListing(list);
        }

        private static string? RetriveDataFromRestAPI(string url, string token)
        {
            var client = new RestClient(url);
            var request = new RestRequest();
            request.AddHeader("accept", "application/json");
            request.AddHeader("authorization", token);
            var response = client.Execute(request);
            return response.Content;
        }
    }
}
