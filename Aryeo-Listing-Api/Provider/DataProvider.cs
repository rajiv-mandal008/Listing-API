using Aryeo_Listing_Api.Model;
using Microsoft.EntityFrameworkCore;

namespace Aryeo_Listing_Api.Provider
{
    public class DataProvider : IDataProvider
    {
        protected readonly IConfiguration Configuration;
        private readonly ApplicationDBContext _db;
        private readonly ILogger<RestAPIProvider> _logger;
        public DataProvider(IConfiguration configuration, ApplicationDBContext db, ILogger<RestAPIProvider> logger)
        {
            Configuration = configuration;
            _db = db;
            _logger = logger;
        }

        public void SaveListing(List<ListingDetails> list)
        {
            try
            {
                foreach (var item in list)
                {
                    //check listing
                    var listing = _db.ListingDetails.Where(p => p.Id == item.Id).AsNoTracking().FirstOrDefault();

                    if (listing == null)
                    {
                        //Save Address
                        var add = _db.AddressDetails.Where(p => p.Id == item.AddressId).AsNoTracking().FirstOrDefault();
                        if (add == null)
                        {
                            AddressDetails address = item.Address;
                            _db.AddressDetails.Add(address);
                        }

                        //Save Lot
                        LotDetails lot = item.LotDetails;
                        _db.LotDetails.Add(lot);

                        //Save  Building
                        BuildingDetails building = item.Building;
                        _db.BuildingDetails.Add(building);

                        //Save Listing Details                
                        item.LotId = lot.Id;
                        item.BuildingId = building.Id;
                        _db.ListingDetails.Add(item);

                        _db.SaveChanges();
                    }
                    else
                    {
                        UpdateAddress(listing.AddressId, item.Address);

                        UpdateLot(listing.LotId, item.LotDetails);

                        UpdateBuilding(listing.BuildingId, item.Building);

                        UpdateListingDetails(listing, item);

                        _db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        private void UpdateLot(long? lotId, LotDetails? newLot)
        {
            var lot = _db.LotDetails.Where(p => p.Id == lotId).FirstOrDefault();
            if (lot != null)
            {
                lot.Lot_Size_Acres = newLot.Lot_Size_Acres;
                lot.Lot_Open_Parking_Spaces = newLot.Lot_Open_Parking_Spaces;
                _db.LotDetails.Update(lot);
            }
        }

        private void UpdateBuilding(long buildingId, BuildingDetails? newBuilding)
        {
            var building = _db.BuildingDetails.Where(p => p.Id == buildingId).FirstOrDefault();
            if (building != null)
            {
                building.Bedrooms = newBuilding.Bedrooms;
                building.Bathrooms = newBuilding.Bathrooms;
                building.Square_Feet = newBuilding.Square_Feet;
                building.Year_Built = newBuilding.Year_Built;

                _db.BuildingDetails.Update(building);
            }
        }

        private void UpdateListingDetails(ListingDetails oldListing, ListingDetails newListing)
        {
            oldListing.Mls_Number = newListing.Mls_Number;
            oldListing.Type = newListing.Type;
            oldListing.Sub_Type = newListing.Sub_Type;
            oldListing.Status = newListing.Status;
            oldListing.Standard_Status = newListing.Standard_Status;
            oldListing.Thumbnail_URL = newListing.Thumbnail_URL;
            oldListing.Large_Thumbnail_URL = newListing.Large_Thumbnail_URL;
            oldListing.Description = newListing.Description;
            oldListing.Floor_Plans = newListing.Floor_Plans;
            oldListing.Interactive_Content = newListing.Interactive_Content;
            oldListing.Downloads_Enabled = newListing.Downloads_Enabled;
            _db.ListingDetails.Update(oldListing);
        }

        private void UpdateAddress(string? addressId, AddressDetails? newAdd)
        {
            var add = _db.AddressDetails.Where(p => p.Id == addressId).AsNoTracking().FirstOrDefault();
            if (add != null)
            {
                add = newAdd;
                add.Id = addressId;
                _db.ChangeTracker.Clear();
                _db.AddressDetails.Update(add);
            }
        }
    }
}
