using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TripBlazrConsole.Models
{
    public class MenuGroup
    {
        //public MenuGroup()
        //{
        //    this.Tags = new HashSet<Tag>();
            
        //}
        [Key]
        public int MenuGroupId { get; set; }

        [Required]
        public string Name { get; set; }

        public int? SortId { get; set; }

        public virtual ICollection<TagMenuGroup> TagMenuGroups { get; set; }
    }
}
