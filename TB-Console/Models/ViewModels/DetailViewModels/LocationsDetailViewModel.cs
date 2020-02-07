using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TripBlazrConsole.Models.ViewModels.LocationViewModels
{
    public class LocationsDetailViewModel
    {
        public Location Location { get; set; }

        public List<Tag> Tags { get; set; }

        public List<Category> Categories { get; set; }

        public List<Hours> Hours { get; set; }
    }
}
