using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TripBlazrConsole.Models
{
    public class Tag
    {
        [Required]
        public int TagId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Image { get; set; }

        [Required]
        public int AccountId { get; set; }
    }
}
