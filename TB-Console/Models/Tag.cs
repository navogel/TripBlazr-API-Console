using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TripBlazrConsole.Models
{
    public class Tag
    {
        //public Tag()
        //{
        //    this.Locations = new HashSet<Location>();
        //    this.MenuGroups = new HashSet<MenuGroup>();
        //}
        [Key]
        public int TagId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Image { get; set; }

        [Required]
        public int AccountId { get; set; }

        public Account Account { get; set; }

        public virtual ICollection<LocationTag> LocationTags { get; set; }

        public virtual ICollection<TagMenuGroup> TagMenuGroups { get; set; }
    }
}
