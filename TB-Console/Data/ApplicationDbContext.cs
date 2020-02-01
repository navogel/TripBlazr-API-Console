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
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<RefreshToken> RefreshToken { get; set; }

        public DbSet<Account> Account { get; set; }

       //public DbSet<AccountUser> AccountUser { get; set; }

        public DbSet<Category> Category { get; set; }

        public DbSet<Location> Location { get; set; }

      //  public DbSet<LocationCategory> LocationCategories { get; set; }

     //   public DbSet<LocationTag> LocationTags { get; set; }

        public DbSet<MenuGroup> MenuGroup { get; set; }

        public DbSet<Tag> Tag { get; set; }

      //  public DbSet<TagMenuGroup> TagMenuGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            //{
            //    relationship.DeleteBehavior = DeleteBehavior.Restrict;
            //}



            //

            modelBuilder.Entity<LocationTag>()
                .HasKey(bc => new { bc.LocationId, bc.TagId });
                

            modelBuilder.Entity<LocationTag>()
                .HasOne(bc => bc.Location)
                .WithMany(b => b.LocationTags)
                .HasForeignKey(bc => bc.LocationId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<LocationTag>()
                .HasOne(bc => bc.Tag)
                .WithMany(c => c.LocationTags)
                .HasForeignKey(bc => bc.TagId)
                .OnDelete(DeleteBehavior.Restrict);

            //

            modelBuilder.Entity<AccountUser>()
                .HasKey(bc => new { bc.AccountId, bc.ApplicationUserId });

            modelBuilder.Entity<AccountUser>()
                .HasOne(bc => bc.Account)
                .WithMany(b => b.AccountUsers)
                .HasForeignKey(bc => bc.AccountId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AccountUser>()
                .HasOne(bc => bc.ApplicationUser)
                .WithMany(c => c.AccountUsers)
                .HasForeignKey(bc => bc.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict);

            //

            modelBuilder.Entity<LocationCategory>()
               .HasKey(bc => new { bc.LocationId, bc.CategoryId });

            modelBuilder.Entity<LocationCategory>()
                .HasOne(bc => bc.Location)
                .WithMany(b => b.LocationCategories)
                .HasForeignKey(bc => bc.LocationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LocationCategory>()
                .HasOne(bc => bc.Category)
                .WithMany(c => c.LocationCategories)
                .HasForeignKey(bc => bc.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            //

            modelBuilder.Entity<TagMenuGroup>()
               .HasKey(bc => new { bc.TagId, bc.MenuGroupId });

            modelBuilder.Entity<TagMenuGroup>()
                .HasOne(bc => bc.Tag)
                .WithMany(b => b.TagMenuGroups)
                .HasForeignKey(bc => bc.TagId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TagMenuGroup>()
                .HasOne(bc => bc.MenuGroup)
                .WithMany(c => c.TagMenuGroups)
                .HasForeignKey(bc => bc.MenuGroupId)
                .OnDelete(DeleteBehavior.Restrict);

            //allowing cascades from main tables to joins

            //modelBuilder.Entity<Location>()
            //    .HasMany(l => l.LocationTags)
            //    .WithOne(lt => lt.Location)
            //    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<Location>()
            //   .HasMany(l => l.LocationCategories)
            //   .WithOne(lt => lt.Location)
            //   .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<Category>()
            //   .HasMany(l => l.LocationCategories)
            //   .WithOne(lt => lt.Category)
            //   .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Tag>()
                .HasMany(t => t.LocationTags)
                .WithOne(l => l.Tag)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Tag>()
                .HasMany(t => t.TagMenuGroups)
                .WithOne(l => l.Tag)
                .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<MenuGroup>()
            //   .HasMany(l => l.TagMenuGroups)
            //   .WithOne(lt => lt.MenuGroup)
            //   .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<Account>()
            //   .HasMany(l => l.AccountUsers)
            //   .WithOne(lt => lt.Account)
            //   .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<ApplicationUser>()
            //   .HasMany(l => l.AccountUsers)
            //   .WithOne(lt => lt.ApplicationUser)
            //   .OnDelete(DeleteBehavior.Restrict);



        }
    }
}
