using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TripBlazrConsole.Models.ViewModels.TagViewModels
{
    public class MenuTagsViewModel
    {
        public int MenuGroupId { get; set; }

        public string Name { get; set; }

        public int? SortId { get; set; }

        public List<Tag> Tags { get; set; } = new List<Tag>();
    }
}
