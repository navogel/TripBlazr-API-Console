using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TripBlazrConsole.Models
{
    public class TagMenuGroup
    {
        [Required]
        public int TagMenuGroupId { get; set; }
        
        [Required]
        public int MenuGroupId { get; set; }

        [Required]
        public int AccountId { get; set; }

        [Required]
        public int TagId { get; set; }
    }
}
