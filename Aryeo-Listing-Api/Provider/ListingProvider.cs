using Aryeo_Listing_Api.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Aryeo_Listing_Api.Provider
{
    public class ListingProvider : IListingProvider
    {
        protected readonly IConfiguration Configuration;
        private readonly ApplicationDBContext _db;
        public ListingProvider(IConfiguration configuration, ApplicationDBContext db)
        {
            Configuration = configuration;
            _db = db;
        }
        public void GetListingList(RequestListing input)
        {
            var client = new RestClient("https://api.aryeo.com/v1/listings");
            var request = new RestRequest();
            request.AddHeader("accept", "application/json");
            request.AddHeader("authorization", "Bearer abc"); //replace abc with correct token
            var response = client.Execute(request);

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore,
            };
            var jsonModel = JsonConvert.DeserializeObject<JObject>(response.Content, settings);
            try
            {
                List<ListingDetails> list = new();
                if (jsonModel["status"].ToString() == "success")
                {
                    foreach (JObject item in jsonModel["data"])
                    {
                        if (item != null)
                        {
                            AddressDetails address = new();
                            JObject jsonAddress = (JObject)item["address"];
                            address.Id = Convert.ToString(jsonAddress.GetValue("id"));
                            address.Latitude = Convert.ToDecimal(jsonAddress.GetValue("latitude"));
                            address.Longitude = Convert.ToDecimal(jsonAddress.GetValue("longitude"));
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
                            address.IS_Map_Dirty = Convert.ToBoolean(jsonAddress.GetValue("is_map_dirty"));

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
                            var price= Convert.ToString(jsonPrice.GetValue("list_price"));

                            ListingDetails data = new();
                            data.Address = address;
                            data.Id = Convert.ToString(item.GetValue("id"));
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
                            data.Downloads_Enabled = Convert.ToBoolean(item.GetValue("downloads_enabled"));

                            list.Add(data);
                        }                        
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void SaveListing(ListingDetails input)
        {

        }
    }
}
