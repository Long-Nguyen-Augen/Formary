using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Formary.Models
{
    public class Step: BaseEntity
    {
        [JsonProperty(PropertyName = "location")]
        [Required]
        [StringLength(45)]
        public string Location { get; set; }

        [JsonProperty(PropertyName = "date")]        
        [DataType(DataType.Text)]        
        public DateTime Date { get; set; }
    }
}
