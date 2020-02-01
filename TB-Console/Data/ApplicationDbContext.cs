using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TripBlazrConsole.Models;
using TripBlazrConsole.Models.Data;

namespace TripBlazrConsole.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<RefreshToken> RefreshToken { get; set; }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<AccountUser> AccountUser { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<LocationCategory> LocationCategories { get; set; }

        public DbSet<LocationTag> LocationTags { get; set; }

        public DbSet<MenuGroup> MenuGroups { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<TagMenuGroup> TagMenuGroups { get; set; }
    }
}
