using System;
using System.Collections.Generic;
using System.Linq;
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
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<RefreshToken> RefreshToken { get; set; }

        public DbSet<Account> Account { get; set; }

       public DbSet<AccountUser> AccountUser { get; set; }

        public DbSet<Category> Category { get; set; }

        public DbSet<Hours> Hours { get; set; }

        public DbSet<Location> Location { get; set; }

        public DbSet<LocationCategory> LocationCategory { get; set; }

        public DbSet<LocationTag> LocationTag { get; set; }

        public DbSet<MenuTagsDetailsViewModel> MenuGroup { get; set; }

        public DbSet<Tag> Tag { get; set; }

        public DbSet<TagMenuGroup> TagMenuGroup { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Location>()
               .Property(b => b.DateCreated)
               .HasDefaultValueSql("GETDATE()");


            modelBuilder.Entity<LocationTag>()
                .HasKey(bc => new { bc.LocationId, bc.TagId });


            modelBuilder.Entity<LocationTag>()
                .HasOne(bc => bc.Location)
                .WithMany(b => b.LocationTags)
                .HasForeignKey(bc => bc.LocationId);


            modelBuilder.Entity<LocationTag>()
                .HasOne(bc => bc.Tag)
                .WithMany(c => c.LocationTags)
                .HasForeignKey(bc => bc.TagId);

            //

            modelBuilder.Entity<AccountUser>()
                .HasKey(bc => new { bc.AccountId, bc.ApplicationUserId });

            modelBuilder.Entity<AccountUser>()
                .HasOne(bc => bc.Account)
                .WithMany(b => b.AccountUsers)
                .HasForeignKey(bc => bc.AccountId);

            modelBuilder.Entity<AccountUser>()
                .HasOne(bc => bc.ApplicationUser)
                .WithMany(c => c.AccountUsers)
                .HasForeignKey(bc => bc.ApplicationUserId);

            //

            modelBuilder.Entity<LocationCategory>()
               .HasKey(bc => new { bc.LocationId, bc.CategoryId });

            modelBuilder.Entity<LocationCategory>()
                .HasOne(bc => bc.Location)
                .WithMany(b => b.LocationCategories)
                .HasForeignKey(bc => bc.LocationId);

            modelBuilder.Entity<LocationCategory>()
                .HasOne(bc => bc.Category)
                .WithMany(c => c.LocationCategories)
                .HasForeignKey(bc => bc.CategoryId);

            //

            modelBuilder.Entity<TagMenuGroup>()
               .HasKey(bc => new { bc.TagId, bc.MenuGroupId });

            modelBuilder.Entity<TagMenuGroup>()
                .HasOne(bc => bc.Tag)
                .WithMany(b => b.TagMenuGroups)
                .HasForeignKey(bc => bc.TagId);

            modelBuilder.Entity<TagMenuGroup>()
                .HasOne(bc => bc.MenuGroup)
                .WithMany(c => c.TagMenuGroups)
                .HasForeignKey(bc => bc.MenuGroupId);

            //allowing cascades from main tables to joins

            modelBuilder.Entity<Location>()
                .HasMany(l => l.LocationTags)
                .WithOne(lt => lt.Location)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Location>()
               .HasMany(l => l.LocationCategories)
               .WithOne(lt => lt.Location)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Category>()
               .HasMany(l => l.LocationCategories)
               .WithOne(lt => lt.Category)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Tag>()
                .HasMany(t => t.LocationTags)
                .WithOne(l => l.Tag)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Tag>()
                .HasMany(t => t.TagMenuGroups)
                .WithOne(l => l.Tag)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MenuTagsDetailsViewModel>()
               .HasMany(l => l.TagMenuGroups)
               .WithOne(lt => lt.MenuGroup)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ApplicationUser>()
               .HasMany(l => l.AccountUsers)
               .WithOne(lt => lt.ApplicationUser)
               .OnDelete(DeleteBehavior.Cascade);

            //restrict cascades on account

            modelBuilder.Entity<Account>()
               .HasMany(l => l.AccountUsers)
               .WithOne(lt => lt.Account)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Account>()
               .HasMany(l => l.Tags)
               .WithOne(lt => lt.Account)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Account>()
               .HasMany(l => l.Locations)
               .WithOne(lt => lt.Account)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Account>()
               .HasMany(l => l.MenuGroups)
               .WithOne(lt => lt.Account)
              .OnDelete(DeleteBehavior.Restrict);

            //force unique citySlug

            modelBuilder.Entity<Account>()
               .HasIndex(u => u.CitySlug)
               .IsUnique();

            modelBuilder.Entity<Account>().HasData(
                new Account
                {
                    AccountId = 1,
                    Name = "NCVC",
                    City = "Nashville",
                    Latitude = 36.1627,
                    Longitude = 86.7816,
                    Inactive = false,
                    IsDeleted = false,
                    CitySlug = "this-is-nashville-slug"
                });

        }
    }
}
