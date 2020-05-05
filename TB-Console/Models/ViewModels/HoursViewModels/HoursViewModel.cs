using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TripBlazrConsole.Models.ViewModels.HoursViewModels
{
    public class HoursViewModel
    {
        public int HoursId { get; set; }

        [Required]

        public int LocationId { get; set; }

        [Required]
        [Range(1, 7, ErrorMessage = "Day Code must be between 1 and 7")]
        public int DayCode { get; set; }

        public string Open { get; set; }

        public string Close { get; set; }

        public bool Is24Hours { get; set; }

        public bool IsClosed { get; set; }
    }
}
