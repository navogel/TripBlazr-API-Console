using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TripBlazrConsole.Models
{
    public class LocationCategory
    {
        


        [Required]
        public int LocationId { get; set; }
        
        [JsonIgnore]
        public Location Location { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [JsonIgnore]
        public Category Category { get; set; }

        public bool IsPrimary { get; set; }

    }
}
