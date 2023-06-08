using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aryeo_Listing_Api.Model
{
    [Table("ListingDetails")]
    public class ListingDetails
    {
        public string Id { get; set; }       

        [ForeignKey("AddressDetails")]
        public string AddressId { get; set; }
        [JsonIgnore]
        public AddressDetails? Address { get; set; }
        public string? Mls_Number { get; set; }
        public string? Type { get; set; }
        public string? Sub_Type { get; set; }
        public string? Status { get; set; }
        public string? Standard_Status { get; set; }
        public string? Thumbnail_URL { get; set; }
        public string? Large_Thumbnail_URL { get; set; }
        public string? Description { get; set; }
        public string? List_Price { get; set; }

        [ForeignKey("LotDetails")]
        public int LotId { get; set; }

        [JsonIgnore]
        public virtual LotDetails? LotDetails { get; set; }

        [ForeignKey("BuildingDetails")]
        public int BuildingId { get; set; }

        [JsonIgnore]
        public virtual BuildingDetails? Building { get; set; }
        public string? Floor_Plans { get; set; }
        public string? Interactive_Content { get; set; }
        public bool Downloads_Enabled { get; set; }
    }
}
