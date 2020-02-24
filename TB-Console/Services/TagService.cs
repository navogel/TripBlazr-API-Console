using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TripBlazrConsole.Data;
using TripBlazrConsole.Interfaces;
using TripBlazrConsole.Models.Data;
using TripBlazrConsole.Models.ViewModels;
using TripBlazrConsole.Settings;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TripBlazrConsole.Models;

namespace TripBlazrConsole.Services
{
    public class TagService : ITagService
    {
        private readonly ApplicationDbContext _context;

        public TagService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<LocationTagResponse> AddLocationTags(LocationTagRequest request)
        {
            var locationId = request.LocationId;

            List<LocationTag> locationTags = new List<LocationTag>();

            request.Tags.ForEach(tag => locationTags.Add(
                new LocationTag
                {
                    LocationId = locationId,
                    TagId = tag.TagId
                }
                ));

            _context.LocationTag.AddRange(locationTags);

            await _context.SaveChangesAsync();

            var response = new LocationTagResponse
            {
                LocationId = locationId,
                LocationTags = locationTags
            };

            return response;   
        }

        public async Task<LocationTag> DeleteTag(int locationId, int tagId)
        {
            var tagToDelete = await _context.LocationTag
                .FirstOrDefaultAsync(lt => lt.LocationId == locationId && lt.TagId == tagId);

            _context.LocationTag.Remove(tagToDelete);
 
            await _context.SaveChangesAsync();

            return tagToDelete;
        }
    }
}
