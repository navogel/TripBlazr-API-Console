using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TripBlazrConsole.Models
{
    public class TagMenuGroup
    {
        [Key]
        public int TagMenuGroupId { get; set; }
        
        [Required]
        public int MenuGroupId { get; set; }
        
        public MenuGroup MenuGroup { get; set; }

        [Required]
        public int TagId { get; set; }

        public Tag Tag { get; set; }
    }
}
