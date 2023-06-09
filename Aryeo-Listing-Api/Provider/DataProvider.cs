using Aryeo_Listing_Api.Model;
using Microsoft.EntityFrameworkCore;

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
                    //check listing
                    var listing = _db.ListingDetails.Where(p => p.Id == item.Id).FirstOrDefault();
                    if (listing == null)
                    {
                        //Save Address
                        
                        var add = _db.AddressDetails.Where(p => p.Id == item.AddressId).FirstOrDefault();
                        if (add == null)
                        {
                            AddressDetails address = item.Address;
                            _db.Entry(address).State = EntityState.Added;
                            _db.SaveChanges();
                        }
                        else
                        {
                            UpdateAddress(add.Id, item.Address);
                        }

                        //Save Lot
                        LotDetails lot = item.LotDetails;
                        _db.Entry(lot).State = EntityState.Added;
                        _db.SaveChanges();

                        //Save  Building
                        BuildingDetails building = item.Building;
                        _db.Entry(building).State = EntityState.Added;
                        _db.SaveChanges();

                        //Save Listing Details                
                        item.LotId = lot.Id;
                        item.BuildingId = building.Id;
                        _db.Entry(item).State = EntityState.Added;
                        _db.SaveChanges();
                    }
                    else
                    {
                        UpdateAddress(listing.AddressId, item.Address);

                        UpdateLot(listing.LotId, item.LotDetails);

                        UpdateBuilding(listing.BuildingId, item.Building);

                        UpdateListingDetails(listing, item);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void UpdateLot(int? lotId, LotDetails? newLot)
        {
            var lot = _db.LotDetails.Where(p => p.Id == lotId).FirstOrDefault();
            lot.Lot_Size_Acres = newLot.Lot_Size_Acres;
            lot.Lot_Open_Parking_Spaces = newLot.Lot_Open_Parking_Spaces;
            _db.Update(lot);
            _db.SaveChanges();
        }

        private void UpdateBuilding(int buildingId, BuildingDetails? newBuilding)
        {
            var building = _db.BuildingDetails.Where(p => p.Id == buildingId).FirstOrDefault();

            building.Bedrooms = newBuilding.Bedrooms;
            building.Bathrooms = newBuilding.Bathrooms;
            building.Square_Feet = newBuilding.Square_Feet;
            building.Year_Built = newBuilding.Year_Built;

            _db.Update(building);
            _db.SaveChanges();
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
            _db.Update(oldListing);
            _db.SaveChanges();
        }

        private void UpdateAddress(string addressId, AddressDetails? newAdd)
        {
            var oldAdd = _db.AddressDetails.Where(p => p.Id == addressId).FirstOrDefault();

            oldAdd.IS_Map_Dirty = newAdd.IS_Map_Dirty;
            oldAdd.Latitude = newAdd.Latitude;
            oldAdd.Longitude = newAdd.Longitude;
            oldAdd.Street_Number = newAdd.Street_Number;
            oldAdd.Unit_Number = newAdd.Unit_Number;
            oldAdd.Street_Name = newAdd.Street_Name;
            oldAdd.City = newAdd.City;
            oldAdd.City_Region = newAdd.City_Region;
            oldAdd.Country = newAdd.Country;
            oldAdd.Country_Region = newAdd.Country_Region;
            oldAdd.Postal_Code = newAdd.Postal_Code;
            oldAdd.County_Or_Parish = newAdd.County_Or_Parish;
            oldAdd.State_Or_Province = newAdd.State_Or_Province;
            oldAdd.State_Or_Province_Region = newAdd.State_Or_Province_Region;
            oldAdd.Timezone = newAdd.Timezone;
            oldAdd.Unparsed_Address = newAdd.Unparsed_Address;
            _db.Update(oldAdd);
            _db.SaveChanges();
        }
    }
}
