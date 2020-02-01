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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Location>()
                .HasMany(l => l.LocationTags)
                .WithOne(lt => lt.Location)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Location>()
               .HasMany(l => l.LocationCategories)
               .WithOne(lt => lt.Location)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Category>()
               .HasMany(l => l.LocationCategories)
               .WithOne(lt => lt.Category)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Tag>()
                .HasMany(t => t.LocationTags)
                .WithOne(l => l.Tag)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Tag>()
                .HasMany(t => t.TagMenuGroups)
                .WithOne(l => l.Tag)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MenuGroup>()
               .HasMany(l => l.TagMenuGroups)
               .WithOne(lt => lt.MenuGroup)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Account>()
               .HasMany(l => l.AccountUsers)
               .WithOne(lt => lt.Account)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ApplicationUser>()
               .HasMany(l => l.AccountUsers)
               .WithOne(lt => lt.ApplicationUser);
               


        }
    }
}
