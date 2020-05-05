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

        [Required]
        [Range(1, 7, ErrorMessage = "Day Code must be between 1 and 7")]
        public int DayCode { get; set; }

        public string Open { get; set; }

        public string Close { get; set; }

        public bool Is24Hours { get; set; }

        public bool IsClosed { get; set; }
    }
}
