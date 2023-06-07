using Aryeo_Listing_Api.Model;
using Microsoft.EntityFrameworkCore;

namespace Aryeo_Listing_Api.Provider
{
    /// <summary>
    /// Application DB Context Class,Inherits DbContext
    /// </summary>    
    public class ApplicationDBContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public ApplicationDBContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server with connection string from app settings
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }

        public DbSet<ListingDetails> ListingDetails { get; set; }
        public DbSet<BuildingDetails> BuildingDetails { get; set; }
        public DbSet<AddressDetails> AddressDetails { get; set; }
        public DbSet<LotDetails> LotDetails { get; set; }
    }
}
