using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TripBlazrConsole.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        
        [Required]
        public string Name { get; set; }

        public string Image { get; set; }

        [JsonIgnore]
        public ICollection<LocationCategory> LocationCategories { get; set; }
    }

}
