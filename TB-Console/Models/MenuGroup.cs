﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TripBlazrConsole.Models
{
    public class MenuGroup
    {
        [Required]
        public int MenuGroupId { get; set; }

        [Required]
        public string Name { get; set; }

        public int? SortId { get; set; }
    }
}
