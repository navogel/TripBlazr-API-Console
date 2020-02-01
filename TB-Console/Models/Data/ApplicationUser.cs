﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TripBlazrConsole.Models.Data
{
    // ******************* 
    // NOTE: Do NOT return an ApplicationUser as part of a json response.
    // It includes sensitive information on it.
    // Instead, always convert an ApplicationUser to an ApplicationUserViewModel before it get sent as JSON
    public class ApplicationUser : IdentityUser
    {
        //public ApplicationUser()
        //{
        //    this.Accounts = new HashSet<Account>();
        //}
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StreetAddress { get; set; }

        public ICollection<AccountUser> AccountUsers { get; set; }
    }
}
