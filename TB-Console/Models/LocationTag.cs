using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TripBlazrConsole.Models
{
    public class LocationTag
    {
        [Required]
        public int LocationTagId { get; set; }

        [Required]
        public int TagId { get; set; }

        [Required]
        public int LocationId { get; set; }

    }
}