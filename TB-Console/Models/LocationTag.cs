using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TripBlazrConsole.Models
{
    public class LocationTag
    {
        [Key]
        public int LocationTagId { get; set; }

        [Required]
        public int TagId { get; set; }

        public Tag Tag { get; set; }

        [Required]
        public int LocationId { get; set; }

        public Location Location { get; set; }

        

    }
}