using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TripBlazrConsole.Models
{
    public class LocationTag
    {
        
        
        [Required]
        public int TagId { get; set; }
        [JsonIgnore]
        public Tag Tag { get; set; }

        
        
        [Required]
        public int LocationId { get; set; }

        [JsonIgnore]
        public Location Location { get; set; }

        

    }
}