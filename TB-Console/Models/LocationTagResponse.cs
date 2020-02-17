using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TripBlazrConsole.Models
{
    public class LocationTagResponse
    {
        public int LocationId { get; set; }

        public List<LocationTag> LocationTags { get; set; }
    }
}
