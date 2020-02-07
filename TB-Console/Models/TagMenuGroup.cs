using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TripBlazrConsole.Models
{
    public class TagMenuGroup
    {
       
        
        
       //[JsonIgnore]
       [Required]
        public int MenuGroupId { get; set; }
       [JsonIgnore]
        public MenuTagsDetailsViewModel MenuGroup { get; set; }

        [Required]
        
        public int TagId { get; set; }
        [JsonIgnore]
        public Tag Tag { get; set; }
    }
}
