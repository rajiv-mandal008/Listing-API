using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Aryeo_Listing_Api.Model
{
    [Table("BuildingDetails")]
    public class BuildingDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }
        public string? Bedrooms { get; set; }
        public string? Bathrooms { get; set; }
        public string? Square_Feet { get; set; }
        public string? Year_Built { get; set; }
    }
}
