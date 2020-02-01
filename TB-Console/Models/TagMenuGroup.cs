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
        //[Key]
        //public int TagMenuGroupId { get; set; }
        
        
       // [JsonIgnore]
        public int MenuGroupId { get; set; }
       // [JsonIgnore]
        public MenuGroup MenuGroup { get; set; }

     
       // [JsonIgnore]
        public int TagId { get; set; }

        public Tag Tag { get; set; }
    }
}
