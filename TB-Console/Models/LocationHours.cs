using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TripBlazrConsole.Models
{
    public class LocationHours
    {
        public int LocationId { get; set; }

        public List<Hours> Hours { get; set; }
    }
}
