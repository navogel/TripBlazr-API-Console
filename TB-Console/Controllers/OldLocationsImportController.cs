using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TripBlazrConsole.Data;
using TripBlazrConsole.Models;
using TripBlazrConsole.Models.ViewModels.LocationViewModels;
//using TripBlazrConsole.Routes.V1;

namespace TripBlazrConsole.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OldLocationsImportController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OldLocationsImportController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Locations
        [HttpGet]
        public async Task<string> GetLocations()
        {
            var oldTBLocationsURL = "https://tripblazrapi.com/api/places/getAllPlaces";

            using (HttpClient client = new HttpClient())
            {
                string data = "";
                IList<LegacyPlace> legacyPlaces = new List<LegacyPlace>();
                IList<Location> locations = new List<Location>();
                try
                {
                    var response = await client.GetAsync(oldTBLocationsURL);
                    data = await response.Content.ReadAsStringAsync();
                    legacyPlaces = JsonConvert.DeserializeObject<IEnumerable<LegacyPlace>>(data).ToList();

                    foreach (var legacyPlace in legacyPlaces)
                    {

                        CreateCategoryIfDoesNotExist(legacyPlace.Tags);
                        CreateTagIfDoesNotExist(legacyPlace.Tags);

                        Location newLocation = new Location 
                        {
                            AccountId = 1,
                            Name = legacyPlace.Title,
                            PhoneNumber = legacyPlace.PhoneNumber,
                            Website = legacyPlace.Website,
                            ShortSummary = legacyPlace.Summary,
                            Description = legacyPlace.Description,
                            Latitude = legacyPlace.Latitude,
                            Longitude = legacyPlace.Longitude,
                            SortId = legacyPlace.SortId,
                            VideoId = GetVideoId(legacyPlace.VideoUrl),
                            VideoStartTime = legacyPlace.VideoStartTime,
                            VideoEndTime = legacyPlace.VideoEndTime,
                            ImageUrl = legacyPlace.ImageUrl,
                            Inactive = legacyPlace.IsLive,
                            IsDeleted = false
                        };
                        
                        newLocation = AddLocationAddress(newLocation, legacyPlace.Address);
                        var addedLocation = _context.Location.Add(newLocation);
                        _context.SaveChanges();
                        CreateLocationHours(addedLocation.Entity.LocationId, legacyPlace.Hours);
                        CreateLocationTags(addedLocation.Entity.LocationId, legacyPlace.Tags);
                        CreateLocationCategories(addedLocation.Entity.LocationId, legacyPlace.Tags);
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                }

                return _context.Location.Count().ToString();
            }
        }
        
        private string GetVideoId(string videoUrl)
        {
            var videoId = "";
            var start = videoUrl.IndexOf("embed/") + "embed/".Length;
            var end = videoUrl.IndexOf("?");
            videoId = videoUrl.Substring(start, end-start);
            return videoId;
        }

        private void CreateCategoryIfDoesNotExist(string tagsList)
        {
            var legacyCategory = tagsList.Split(",")[1];
            if (!_context.Category.Any(c => c.Name.ToLower() == legacyCategory.ToLower()))
            {
                _context.Add(
                    new Category
                    {
                        Name = legacyCategory.ToLower(),
                        Image = "No Image Yet"
                    });
                _context.SaveChanges();
            }
        }

        private void CreateTagIfDoesNotExist(string tagsList)
        {
            var legacyTags = tagsList.Split(",");
            legacyTags = legacyTags.Skip(2).ToArray();

            foreach (var tag in legacyTags)
            {
                if (!_context.Category.Any(c => c.Name.ToLower() == tag.ToLower().Trim()))
                {
                    if (!_context.Tag.Any(t => t.Name.ToLower() == tag.ToLower().Trim()))
                    {
                        _context.Add(
                            new Tag
                            {
                                Name = tag.ToLower().Trim(),
                                Image = "No Image",
                                AccountId = 1
                            });
                        _context.SaveChanges();
                    }
                }
            }
        }

        private void CreateLocationTags(int locationId, string tagsList)
        {
            var legacyTags = tagsList.Split(",");
            legacyTags = legacyTags.Skip(2).ToArray();

            foreach (var tag in legacyTags)
            {
                if (!_context.Category.Any(c => c.Name.ToLower() == tag.ToLower().Trim()))
                {
                    var newTag = _context.Tag.Where(t => t.Name.Trim().ToLower() == tag.ToLower().Trim()).First();
                    _context.LocationTag.Add( new LocationTag{TagId = newTag.TagId, LocationId = locationId});
                }
            }
            _context.SaveChanges();
        }

        private void CreateLocationCategories(int locationId, string tagsList)
        {
            var legacyTags = tagsList.Split(",");
            legacyTags = legacyTags.Skip(1).ToArray();

            foreach (var tag in legacyTags)
            {
                if (!_context.Category.Any(c => c.Name.Trim().ToLower() == tag.Trim().ToLower())){
                    break;
                }
                var category = _context.Category
                    .Where(c => c.Name.Trim().ToLower() == tag.Trim().ToLower())
                    .First();
                if (_context.LocationCategory.Any(lc => lc.LocationId == locationId))
                {
                    _context.LocationCategory.Add(new LocationCategory{
                        LocationId = locationId,
                        CategoryId = category.CategoryId,
                        IsPrimary = false
                    });
                } else {
                    _context.LocationCategory.Add(new LocationCategory{
                        LocationId = locationId,
                        CategoryId = category.CategoryId,
                        IsPrimary = true
                    });
                }
                _context.SaveChanges();
            }
            _context.SaveChanges();
        }

        private void CreateLocationHours(int locationId, string hours)
        {
            if (hours is null)
            {
                return;
            }
            
            var hoursList = hours.Split("---");

            if (hoursList.Length == 7)
            {
                foreach (var day in hoursList)
                {
                    var hyphenIndex = day.IndexOf("-");
                    var time = day.Substring(4).Trim();
                    switch(day.Substring(0, 4))
                    {
                        case "Mon:":
                        if (hyphenIndex == -1) {
                            Create24HoursOrClosed(locationId, 1, time);
                            break;
                        } else {
                            CreateDailyHours(locationId, 1, time, hyphenIndex);
                        }
                        break;
                        case "Tue:":
                        if (hyphenIndex == -1) {
                            Create24HoursOrClosed(locationId, 2, time);
                            break;
                        } else {
                            CreateDailyHours(locationId, 2, time, hyphenIndex);
                        }
                        break;
                        case "Wed:":
                        if (hyphenIndex == -1) {
                            Create24HoursOrClosed(locationId, 3, time);
                            break;
                        } else {
                            CreateDailyHours(locationId, 3, time, hyphenIndex);
                        }
                        break;
                        case "Thu:":
                        if (hyphenIndex == -1) {
                            Create24HoursOrClosed(locationId, 4, time);
                            break;
                        } else {
                            CreateDailyHours(locationId, 4, time, hyphenIndex);
                        }
                        break;
                        case "Fri:":
                        if (hyphenIndex == -1) {
                            Create24HoursOrClosed(locationId, 5, time);
                            break;
                        } else {
                            CreateDailyHours(locationId, 5, time, hyphenIndex);
                        }
                        break;
                        case "Sat:":
                        if (hyphenIndex == -1) {
                            Create24HoursOrClosed(locationId, 6, time);
                            break;
                        } else {
                            CreateDailyHours(locationId, 6, time, hyphenIndex);
                        }
                        break;
                        case "Sun:":
                        if (hyphenIndex == -1) {
                            Create24HoursOrClosed(locationId, 7, time);
                            break;
                        } else {
                            CreateDailyHours(locationId, 7, time, hyphenIndex);
                        }
                        break;
                        default:
                        break;
                    }
                }
            }
        }

        private void Create24HoursOrClosed(int locationId, int dayCode, string hours)
        {
            if (hours.Trim().ToLower() == "closed"){
                _context.Hours.Add( new Hours{
                    LocationId = locationId,
                    DayCode = dayCode,
                    IsClosed = true
                });
            } else {
                _context.Hours.Add( new Hours{
                    LocationId = locationId,
                    DayCode = dayCode,
                    Is24Hours = true
                });
            }
            _context.SaveChanges();
        }

        private void CreateDailyHours(int locationId, int dayCode, string hours, int hyphenIndex)
        {
            string openTime = null;
            string closeTime = null;

            openTime = DateTime.Parse(hours.Split("-")[0].Trim()).ToString("HH:mm");
            closeTime = DateTime.Parse(hours.Split("-")[1].Trim()).ToString("HH:mm");

            _context.Hours.Add( new Hours{
                LocationId = locationId,
                DayCode = dayCode,
                Open = openTime,
                Close = closeTime
            });
            _context.SaveChanges();
        }

        private Location AddLocationAddress(Location newLocation, string address)
        {
            if (address is null)
            {
                return newLocation;
            }

            var addressList = address.Split("---");
            if (addressList.Length == 0)
            {
                return newLocation;
            }

            var zipCodeString = address.Substring(address.Length - 5);
            var zipCode = int.Parse(zipCodeString);

            if (addressList.Length < 2)
            {
                newLocation.Address1 = addressList[0];
                newLocation.City = "Nashville";
                newLocation.Zipcode = zipCode;
            } else {
                newLocation.Address1 = addressList[0];
                newLocation.Address2 = addressList[1];
                newLocation.City = "Nashville";
                newLocation.Zipcode = zipCode;
            }

            return newLocation;
        }
    }
}
