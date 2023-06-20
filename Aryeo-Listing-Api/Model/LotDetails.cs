using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Aryeo_Listing_Api.Model
{
    [Table("LotDetails")]
    public class LotDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string? Lot_Size_Acres { get; set; }
        public string? Lot_Open_Parking_Spaces { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<ListingDetails> ListingDetails { get; set; }
    }
}
