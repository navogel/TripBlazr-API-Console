using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TripBlazrConsole.Models
{
    public class LocationCategory
    {
        [Required]
        public int LocationCategoryId { get; set; }

        [Required]
        public int LocationId { get; set; }

        [Required]
        public int CategoryId { get; set; }

    }
}
