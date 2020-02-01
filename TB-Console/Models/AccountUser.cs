using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TripBlazrConsole.Models.Data;

namespace TripBlazrConsole.Models
{
    public class AccountUser
    {
        [Key]

        
        public int AccountUserId { get; set; }

        [Required]
        public int AccountId { get; set; }

        public Account Account { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}
