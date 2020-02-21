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

        public async Task<IEnumerable<LocationViewModel>> GetConsoleLocations(int id, string search, string category, string tag, bool? isActive, string userId)
        {
            //exclude parking lots by videoId
            string excludeId = "8A__mpnWBT8";
            //initial query restricting by account
            var query = _context.Location
                .Include(l => l.Hours)
                .Include(l => l.LocationCategories)
                    .ThenInclude(lc => lc.Category)
                .Include(l => l.LocationTags)
                    .ThenInclude(c => c.Tag)
                .Where(q => q.AccountId == id && q.IsDeleted != true)
                .Where(q => q.VideoId != excludeId)
                //verify user has access to this account
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

            //create location object after filtering
            var locations = await query

            .Select(l => _mapper.Map<LocationViewModel>(l)).ToListAsync();

            return locations;    
        }

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

            //create new location
            _context.Location.Add(location);
            await _context.SaveChangesAsync();

            //check if there is a file attatched

            if (viewModel.File != null && viewModel.File.Length > 0)
            {

                //create filname based on created location ID
                int fileName = location.LocationId;

                //create path and insert image with original filename

                using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\Upload\\" + viewModel.File.FileName))
                {
                    viewModel.File.CopyTo(fileStream);
                    fileStream.Flush();
                }

                //replace original filename with location ID filename, keeping extension

                FileInfo currentFile = new FileInfo(_environment.WebRootPath + "\\Upload\\" + viewModel.File.FileName);
                currentFile.MoveTo(currentFile.Directory.FullName + "\\" + fileName + currentFile.Extension);

                // update location with new filename

                location.ImageUrl = fileName + currentFile.Extension;
                _context.Entry(location).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return location;
            }

            return location;
        }

        public Task<CreateLocationViewModel> EditLocation(CreateLocationViewModel viewModel, int id)
        {
            throw new NotImplementedException();
        }

        public Task<Location> DeleteLocation(int id)
        {
            throw new NotImplementedException();
        }

        public Task EditLocationIsActive(int id)
        {
            throw new NotImplementedException();
        } 
    }
}
