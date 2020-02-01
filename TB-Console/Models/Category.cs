using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TripBlazrConsole.Models
{
    public class Category
    {
        [Required]
        public int CategoryId { get; set; }
        
        [Required]
        public string Name { get; set; }

        public string Image { get; set; }
    }
}
