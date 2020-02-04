using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TripBlazrConsole.Models
{
    public class Tag
    {
        
        [Key]
        public int TagId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Image { get; set; }

        [Required]
        [JsonIgnore]
        public int AccountId { get; set; }

        [JsonIgnore]

        public Account Account { get; set; }

        [JsonIgnore]

        public ICollection<LocationTag> LocationTags { get; set; }

        [JsonIgnore]
        public ICollection<TagMenuGroup> TagMenuGroups { get; set; }
    }
}
