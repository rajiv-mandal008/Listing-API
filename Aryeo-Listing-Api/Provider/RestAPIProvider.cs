using Aryeo_Listing_Api.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Aryeo_Listing_Api.Provider
{
    public class RestAPIProvider : IRestAPIProvider
    {
        protected readonly IConfiguration Configuration;
        private readonly IDataProvider _dataProvider;
        public RestAPIProvider(IConfiguration configuration, IDataProvider dataProvider)
        {
            Configuration = configuration;
            _dataProvider = dataProvider;
        }
        public ResponseListing GetListingList(RequestListing input)
        {
            string url = "https://api.aryeo.com/v1/listings?per_page=100";
            string response = RetriveDataFromRestAPI(url);

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore,
            };
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
                        string childResponse = RetriveDataFromRestAPI(newurl);

                        var childJsonModel = JsonConvert.DeserializeObject<JObject>(childResponse, settings);

                        //Process 2nd onward call data
                        ProccessJsonData(childJsonModel);
                    }

                    ResponseListing obj = new()
                    {
                        ResponseMessage = "Record saved successfuly!",
                        TotalRecordInserted = totalCount
                    };
                    return obj;
                }
                else
                {
                    ResponseListing obj = new()
                    {
                        ResponseMessage = "Record not saved!",
                        TotalRecordInserted = 0
                    };
                    return obj;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task ProccessJsonData(JObject jsonModel)
        {
            List<ListingDetails> list = new();

            foreach (JObject item in jsonModel["data"])
            {
                if (item != null)
                {
                    AddressDetails address = new();
                    JObject jsonAddress = (JObject)item["address"];
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
                    JObject jsonLot = (JObject)item["lot"];
                    lot.Lot_Size_Acres = Convert.ToString(jsonLot.GetValue("lot_size_acres"));
                    lot.Lot_Open_Parking_Spaces = Convert.ToString(jsonLot.GetValue("lot_open_parking_spaces"));

                    BuildingDetails building = new();
                    JObject jsonBuilding = (JObject)item["building"];
                    building.Bedrooms = Convert.ToString(jsonBuilding.GetValue("bedrooms"));
                    building.Bathrooms = Convert.ToString(jsonBuilding.GetValue("bathrooms"));
                    building.Square_Feet = Convert.ToString(jsonBuilding.GetValue("square_feet"));
                    building.Year_Built = Convert.ToString(jsonBuilding.GetValue("year_built"));

                    JObject jsonPrice = (JObject)item["price"];
                    var price = Convert.ToString(jsonPrice.GetValue("list_price"));

                    ListingDetails data = new();
                    data.Address = address;
                    data.Id = Convert.ToString(item.GetValue("id"));
                    data.AddressId = address.Id;
                    data.Mls_Number = Convert.ToString(item.GetValue("mls_number"));
                    data.Type = Convert.ToString(item.GetValue("type"));
                    data.Sub_Type = Convert.ToString(item.GetValue("sub_type"));
                    data.Status = Convert.ToString(item.GetValue("status"));
                    data.Standard_Status = Convert.ToString(item.GetValue("standard_status"));
                    data.Thumbnail_URL = Convert.ToString(item.GetValue("thumbnail_url"));
                    data.Large_Thumbnail_URL = Convert.ToString(item.GetValue("large_thumbnail_url"));
                    data.Description = Convert.ToString(item.GetValue("description"));
                    data.List_Price = price;
                    data.LotDetails = lot;
                    data.Building = building;
                    data.Floor_Plans = Convert.ToString(item.GetValue("floor_plans"));
                    data.Interactive_Content = Convert.ToString(item.GetValue("interactive_content"));
                    data.Downloads_Enabled = jsonAddress.GetValue("downloads_enabled") != null ? Convert.ToBoolean(item.GetValue("downloads_enabled")) : false;

                    list.Add(data);
                }
            }

            _dataProvider.SaveListing(list);
        }

        private static string RetriveDataFromRestAPI(string url)
        {
            var client = new RestClient(url);
            var request = new RestRequest();
            request.AddHeader("accept", "application/json");
            request.AddHeader("authorization", "Bearer abc"); //replace abc with correct token
            var response = client.Execute(request);
            return response.Content;
        }
    }
}
