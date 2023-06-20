using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aryeo_Listing_Api.Model
{
    [Table("AddressDetails")]
    public class AddressDetails
    {
        [Key]
        public string Id { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Street_Number { get; set; }
        public string Street_Name { get; set; }
        public string Unit_Number { get; set; }
        public string Postal_Code { get; set; }
        public string City { get; set; }
        public string City_Region { get; set; }
        public string County_Or_Parish { get; set; }
        public string State_Or_Province { get; set; }
        public string State_Or_Province_Region { get; set; }
        public string Country { get; set; }
        public string Country_Region { get; set; }
        public string Timezone { get; set; }
        public string Unparsed_Address { get; set; }
        public bool IS_Map_Dirty { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<ListingDetails> ListingDetails { get; set; }
    }
}
