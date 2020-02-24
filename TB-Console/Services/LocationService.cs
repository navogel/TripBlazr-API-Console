using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripBlazrConsole.Data;
using TripBlazrConsole.Interfaces;
using TripBlazrConsole.Models;
using TripBlazrConsole.Models.ViewModels.LocationViewModels;
using TripBlazrConsole.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace TripBlazrConsole.Services
{
    public class LocationService : ILocationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private static IWebHostEnvironment _environment;

        public LocationService(ApplicationDbContext context, IMapper mapper, IWebHostEnvironment environment)
        {
            _context = context;
            _mapper = mapper;
            _environment = environment;
        }


        //Client API get all locations

        public async Task<IEnumerable<LocationViewModel>> GetLocations(string citySlug)
        {
            var locationList = await _context.Location
               .Include(l => l.Hours)
               .Include(l => l.LocationCategories)
                    .ThenInclude(lc => lc.Category)
               .Include(l => l.LocationTags)
                     .ThenInclude(c => c.Tag)
               .Where(l => l.Account.CitySlug == citySlug && l.IsDeleted != true && l.IsActive == true)
               .Select(l => _mapper.Map<LocationViewModel>(l)).ToListAsync();

            return locationList;
        }

        //Console API get all locations with UserId check
        public async Task<IEnumerable<LocationViewModel>> GetConsoleLocations(int id, string search, string category, string tag, bool? isActive, string userId)
        {
            string excludeParkingByVideoId = "8A__mpnWBT8";

            var query = _context.Location
                .Include(l => l.Hours)
                .Include(l => l.LocationCategories)
                    .ThenInclude(lc => lc.Category)
                .Include(l => l.LocationTags)
                    .ThenInclude(c => c.Tag)
                .Where(q => q.AccountId == id && q.IsDeleted != true)
                .Where(q => q.VideoId != excludeParkingByVideoId)
                .Where(l => l.Account.AccountUsers.Any(au => au.ApplicationUserId == userId))
                .OrderByDescending(l => l.DateCreated)
                .AsQueryable();

            //NOTE parameter filters --> SAVE for future functionality: Search, Category, Tag Filters

            //if (!string.IsNullOrWhiteSpace(search))
            //{
            //    query = query.Where(q => EF.Functions.Like(q.Name, $"%{search}%")).ToList();
            //};

            //if (!string.IsNullOrWhiteSpace(category))
            //{
            //    query = query.Where(q => q.LocationCategories.Any(lc => lc.Category.Name == category)).ToList();
            //};

            //if (!string.IsNullOrWhiteSpace(tag))
            //{
            //    query = query.Where(q => q.LocationTags.Any(lc => lc.Tag.Name == tag)).ToList();
            //};

            if (isActive == false)
            {
                query = query.Where(q => q.IsActive == false);
            };

            if (isActive == true)
            {
                query = query.Where(q => q.IsActive == true);
            };

            var locations = await query

            .Select(l => _mapper.Map<LocationViewModel>(l)).ToListAsync();

            return locations;    
        }

        //Console API get single location with UserId check
        public async Task<LocationViewModel> GetLocation(int id, string userId)
        {
            var location = await _context.Location
                .Include(l => l.Hours)
                .Include(l => l.LocationCategories)
                    .ThenInclude(lc => lc.Category)
                .Include(l => l.LocationTags)
                    .ThenInclude(lt => lt.Tag)
                .Include(l => l.Account.AccountUsers)
                .Where(l => l.LocationId == id)
                .Where(l => l.Account.AccountUsers.Any(au => au.ApplicationUserId == userId))
                .Select(l => _mapper.Map<LocationViewModel>(l)).FirstOrDefaultAsync();

            return location;
        }

        //Console API create location
        public async Task<Location> PostLocation(CreateLocationViewModel viewModel)
        {
            var location = new Location()
            {
                AccountId = viewModel.AccountId,
                Name = viewModel.Name,
                PhoneNumber = viewModel.PhoneNumber,
                Website = viewModel.Website,
                ShortSummary = viewModel.ShortSummary,
                Description = viewModel.Description,
                Latitude = viewModel.Latitude,
                Longitude = viewModel.Longitude,
                VideoId = viewModel.VideoId,
                VideoStartTime = viewModel.VideoStartTime,
                VideoEndTime = viewModel.VideoEndTime,
                Address1 = viewModel.Address1,
                Address2 = viewModel.Address2,
                City = viewModel.City,
                State = viewModel.State,
                Zipcode = viewModel.Zipcode,
                IsDeleted = false,
                IsActive = false,
                ImageUrl = "logo.png"
            };

            _context.Location.Add(location);
            await _context.SaveChangesAsync();

            if (viewModel.File != null && viewModel.File.Length > 0)
            {
                int newImageFileName = location.LocationId;

                using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\Upload\\" + viewModel.File.FileName))
                {
                    viewModel.File.CopyTo(fileStream);
                    fileStream.Flush();
                }

                FileInfo currentFile = new FileInfo(_environment.WebRootPath + "\\Upload\\" + viewModel.File.FileName);
                currentFile.MoveTo(currentFile.Directory.FullName + "\\" + newImageFileName + currentFile.Extension);

                location.ImageUrl = newImageFileName + currentFile.Extension;

                _context.Entry(location).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return location;
            }

            return location;
        }

        //Console API edit location with userId check
        public async Task<Location> EditLocation(CreateLocationViewModel viewModel, int id, string userId)
        {
            var locationFromDb = await _context.Location
                    .Include(l => l.Account.AccountUsers)
                    .Where(l => l.Account.AccountUsers.Any(au => au.ApplicationUserId == userId))
                    .FirstOrDefaultAsync(l => l.LocationId == id);

            locationFromDb.Name = viewModel.Name;
            locationFromDb.PhoneNumber = viewModel.PhoneNumber;
            locationFromDb.Website = viewModel.Website;
            locationFromDb.ShortSummary = viewModel.ShortSummary;
            locationFromDb.Description = viewModel.Description;
            locationFromDb.Latitude = viewModel.Latitude;
            locationFromDb.Longitude = viewModel.Longitude;
            locationFromDb.SortId = viewModel.SortId;
            locationFromDb.VideoId = viewModel.VideoId;
            locationFromDb.VideoStartTime = viewModel.VideoStartTime;
            locationFromDb.VideoEndTime = viewModel.VideoEndTime;
            locationFromDb.Address1 = viewModel.Address1;
            locationFromDb.Address2 = viewModel.Address2;
            locationFromDb.City = viewModel.City;
            locationFromDb.State = viewModel.State;
            locationFromDb.Zipcode = viewModel.Zipcode;
            locationFromDb.IsDeleted = false;
            locationFromDb.IsActive = viewModel.IsActive;
            locationFromDb.ImageUrl = locationFromDb.ImageUrl;
            locationFromDb.DateEdited = DateTime.Now;
            locationFromDb.SeeWebsite = locationFromDb.SeeWebsite;
            locationFromDb.HoursNotes = locationFromDb.HoursNotes;

            if (viewModel.File != null && viewModel.File.Length > 0)
            {
                int newImageFileName = locationFromDb.LocationId;

                using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\Upload\\" + viewModel.File.FileName))
                {
                    viewModel.File.CopyTo(fileStream);
                    fileStream.Flush();
                }

                FileInfo currentFile = new FileInfo(_environment.WebRootPath + "\\Upload\\" + viewModel.File.FileName);
                currentFile.MoveTo(currentFile.Directory.FullName + "\\" + newImageFileName + currentFile.Extension, true);

                locationFromDb.ImageUrl = newImageFileName + currentFile.Extension;

                _context.Entry(locationFromDb).State = EntityState.Modified;
                await _context.SaveChangesAsync();
               
                return locationFromDb;
            }            

            _context.Entry(locationFromDb).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return locationFromDb;
        }

        //Console API toggle location isActive with UserID
        public async Task<Location> EditLocationIsActive(int id, string userId)
        {
            var location = await _context.Location
                    .Include(l => l.Account.AccountUsers)
                    .Where(l => l.Account.AccountUsers.Any(au => au.ApplicationUserId == userId))
                    .FirstOrDefaultAsync(l => l.LocationId == id);

            if (location.IsActive == false)
            {
                location.IsActive = true;
            }
            else
            {
                location.IsActive = false;
            }

            _context.Entry(location).State = EntityState.Modified;
        
            await _context.SaveChangesAsync();
            
            return location;
        }

        //Console API soft delete with userId check
        public async Task<Location> DeleteLocation(int id, string userId)
        {
            var location = await _context.Location
                    .Include(l => l.Account.AccountUsers)
                    .Where(l => l.Account.AccountUsers.Any(au => au.ApplicationUserId == userId))
                    .FirstOrDefaultAsync(l => l.LocationId == id);

            location.IsDeleted = true;

            _context.Update(location);

            await _context.SaveChangesAsync();
            
            return location;
        }
    }
}
