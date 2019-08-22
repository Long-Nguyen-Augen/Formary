namespace Formary.Models
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class Delivery: BaseEntity
    {        
        [JsonProperty(PropertyName = "referencenum")]
        [Required]
        public string Reference { get; set; }
        
        [JsonProperty(PropertyName = "total_weight")]
        [Display(Name = "Total Weight")]
        [Range(0, int.MaxValue)]
        public decimal TotalWeight { get; set; }

        [JsonProperty(PropertyName = "deliverydate")]
        [Required]
        [DataType(DataType.Text)]        
        [Display(Name = "Delivery date")]
        public DateTime DeliveryDate { get; set; }
    }
}
