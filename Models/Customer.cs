using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Formary.Models
{
    public class Customer: BaseEntity
    {
        [JsonProperty(PropertyName = "name")]
        [Required]
        [StringLength(45)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "contact")]
        [StringLength(45)]
        public string Contact { get; set; }

        [JsonProperty(PropertyName = "email")]
        [StringLength(45)]
        public string Email { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
