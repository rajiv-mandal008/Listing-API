using Aryeo_Listing_Api.Model;
using Microsoft.EntityFrameworkCore;

namespace Aryeo_Listing_Api.Provider
{
    /// <summary>
    /// Application DB Context Class,Inherits DbContext
    /// </summary>    
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
            InitalizeContext();
        }

        protected virtual void InitalizeContext()
        {
            ChangeTracker.AutoDetectChangesEnabled = false;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            //Database.SetCommandTimeout(360);
        }        

        public DbSet<ListingDetails> ListingDetails { get; set; }
        public DbSet<BuildingDetails> BuildingDetails { get; set; }
        public DbSet<AddressDetails> AddressDetails { get; set; }
        public DbSet<LotDetails> LotDetails { get; set; }
    }
}
