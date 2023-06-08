using Aryeo_Listing_Api.Model;

namespace Aryeo_Listing_Api.Provider
{
    public class DataProvider : IDataProvider
    {
        protected readonly IConfiguration Configuration;
        private readonly ApplicationDBContext _db;
        public DataProvider(IConfiguration configuration, ApplicationDBContext db)
        {
            Configuration = configuration;
            _db = db;
        }        

        public void SaveListing(List<ListingDetails> list)
        {
            try
            {
                foreach (var item in list)
                {
                    //Save Address
                    AddressDetails adress = item.Address;
                    _db.Add(adress);
                    _db.SaveChanges();

                    //Save Lot
                    LotDetails lot = item.LotDetails;
                    _db.Add(lot);
                    int lotId = _db.SaveChanges();

                    //Save  Building
                    BuildingDetails building = item.Building;
                    _db.Add(building);
                    int buildingId = _db.SaveChanges();

                    //Save Listing Details                
                    item.LotId = lotId;
                    item.BuildingId = buildingId;
                    _db.Add(item);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
