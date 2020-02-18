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

namespace TripBlazrConsole.Services
{
    public class LocationService : ILocationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public LocationService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

        public Task<IEnumerable<LocationViewModel>> GetConsoleLocations(int id, string search, string category, string tag, bool? isActive)
        {
            throw new NotImplementedException();
        }

        public Task<LocationsDetailViewModel> GetLocation(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Location> DeleteLocation(int id)
        {
            throw new NotImplementedException();
        }

        public Task<CreateLocationViewModel> EditLocation(CreateLocationViewModel viewModel, int id)
        {
            throw new NotImplementedException();
        }

        public Task EditLocationIsActive(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Location> UploadImage(IFormFile file, int id)
        {
            throw new NotImplementedException();
        }






    }
}
