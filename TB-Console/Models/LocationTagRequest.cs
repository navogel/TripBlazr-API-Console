using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TripBlazrConsole.Models
{
    public class LocationTagRequest
    {
        public int LocationId { get; set; }

        public List<Tag> Tags { get; set; }
    }
}
