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
        //[Key]
        //public int LocationCategoryId { get; set; }


        [Required]
        [JsonIgnore]
        public int LocationId { get; set; }
        [JsonIgnore]
        public Location Location { get; set; }

        [Required]
        [JsonIgnore]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

    }
}
