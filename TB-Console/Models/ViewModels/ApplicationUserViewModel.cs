using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TripBlazrConsole.Models.ViewModels
{
    public class ApplicationUserViewModel
    {
        //public ApplicationUserViewModel()
        //{
        //    this.Accounts = new HashSet<Account>();
        //}
        public string Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //public virtual ICollection<Account> Accounts { get; set; }
    }
}
