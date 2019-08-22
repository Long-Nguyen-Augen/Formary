using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Formary.Models
{
    public class BaseEntity
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
    }
}
