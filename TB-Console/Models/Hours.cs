using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TripBlazrConsole.Models
{
    public class Hours
    {
        public int HoursId { get; set; }

        [Required]

        [JsonIgnore]


        public int LocationId { get; set; }

        [JsonIgnore]

        public Location Location { get; set; }

        public int DayCode { get; set; }

        [Required]

        public string Open { get; set; }

        [Required]

        public string Close { get; set; }
    }
}
