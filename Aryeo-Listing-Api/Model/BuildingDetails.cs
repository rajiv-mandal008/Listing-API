using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Aryeo_Listing_Api.Model
{
    [Table("BuildingDetails")]
    public class BuildingDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string? Bedrooms { get; set; }
        public string? Bathrooms { get; set; }
        public string? Square_Feet { get; set; }
        public string? Year_Built { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<ListingDetails> ListingDetails { get; set; }
    }
}
