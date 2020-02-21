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
        public Task<LocationTagResponse> AddLocationTags(LocationTagRequest request)
        {
            try
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

                _context.SaveChanges();

                var response = new LocationTagResponse
                {
                    LocationId = locationId,
                    LocationTags = locationTags
                };

            return Task.FromResult(response);

            } catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<LocationTag> DeleteTag(int locationId, int tagId)
        {
            var tagToDelete = await _context.LocationTag
                .FirstOrDefaultAsync(lt => lt.LocationId == locationId && lt.TagId == tagId);

            _context.LocationTag.Remove(tagToDelete);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return tagToDelete;
        }
    }
}
