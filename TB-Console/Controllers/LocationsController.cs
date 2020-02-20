using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TripBlazrConsole.Data;
using TripBlazrConsole.Helpers;
using TripBlazrConsole.Interfaces;
using TripBlazrConsole.Models;
using TripBlazrConsole.Models.ViewModels.LocationViewModels;
using TripBlazrConsole.Routes.V1;

namespace TripBlazrConsole.Controllers
{
    //[Authorize]
   // [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        private static IWebHostEnvironment _environment;

        private readonly IMapper _mapper;

        private readonly ITagService _tagService;

        private readonly ILocationService _locationService;



        public LocationsController(ApplicationDbContext context, IWebHostEnvironment environment, IMapper mapper, ITagService tagService, ILocationService locationService)
        {
            _context = context;
            _environment = environment;
            _mapper = mapper;
            _tagService = tagService;
            _locationService = locationService;


        }

        // GET: CLIENT: ANON: api/Locations/citySlug
        [AllowAnonymous]
        [HttpGet(Api.Location.GetLocations)]
        public async Task<ActionResult<IEnumerable<LocationViewModel>>> GetLocations(string citySlug)
        {
            try
            {
                var response = await _locationService.GetLocations(citySlug);
                
                return Ok(response);
            }
            catch (Exception e)
            {
                return NotFound(e);
            }
           
        }

        // GET: CONSOLE: PRIVATE: api/Locations/Account/{id}?search/category/tag={params}
        
        [HttpGet(Api.Location.GetConsoleLocations)]
        public async Task<ActionResult<IEnumerable<LocationViewModel>>> GetConsoleLocations(int id, string search, string category, string tag, bool? isActive)
        {
            try
            {
                var userId = HttpContext.GetUserId();

                var response = await _locationService.GetConsoleLocations(id, search, category, tag, isActive, userId);

                return Ok(response);
            }
            catch (Exception e)
            {
                return NotFound(e);
            }
        }

        // GET: CONSOLE: PRIVATE api/Locations/by Id
       
        [HttpGet(Api.Location.GetLocation)]
        public async Task<ActionResult<LocationViewModel>> GetLocation(int id)
        {
            try
            {
                var userId = HttpContext.GetUserId();

                var response = await _locationService.GetLocation(id, userId);

                if (response == null)
                {
                    return NotFound($"No Location found with the ID of {id}");
                }

                return Ok(response);
            }
            catch (Exception e)
            {
                return NotFound(e);
            } 
        }

        // POST: api/Locations  - CREATE LOCATION
        
       
        [HttpPost(Api.Location.PostLocation)]
        public async Task<ActionResult<Location>> PostLocation([FromForm]CreateLocationViewModel viewModel)
        {
            try
            {
                var response = await _locationService.PostLocation(viewModel);

                return CreatedAtAction("GetLocation", new { id = response.LocationId }, response);
            }
            catch (Exception e)
            {
                return NotFound(e);
            }
            //var location = new Location()
            //{
            //    AccountId = viewModel.AccountId,
            //    Name = viewModel.Name,
            //    PhoneNumber = viewModel.PhoneNumber,
            //    Website = viewModel.Website,
            //    ShortSummary = viewModel.ShortSummary,
            //    Description = viewModel.Description,
            //    Latitude = viewModel.Latitude,
            //    Longitude = viewModel.Longitude,
            //    VideoId = viewModel.VideoId,
            //    VideoStartTime = viewModel.VideoStartTime,
            //    VideoEndTime = viewModel.VideoEndTime,
            //    Address1 = viewModel.Address1,
            //    Address2 = viewModel.Address2,
            //    City = viewModel.City,
            //    State = viewModel.State,
            //    Zipcode = viewModel.Zipcode,
            //    IsDeleted = false,
            //    IsActive = false,
            //    ImageUrl = "logo.png"
            //};

            ////create new location
            // _context.Location.Add(location);
            //await _context.SaveChangesAsync();

            ////check if there is a file attatched

            //if (viewModel.File != null && viewModel.File.Length > 0)
            //{

            //    //create filname based on created location ID
            //    int fileName = location.LocationId;

            //   //create path and insert image with original filename

            //    using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\Upload\\" + viewModel.File.FileName))
            //    {
            //        viewModel.File.CopyTo(fileStream);
            //        fileStream.Flush();
            //    }

            //    //replace original filename with location ID filename, keeping extension

            //    FileInfo currentFile = new FileInfo(_environment.WebRootPath + "\\Upload\\" + viewModel.File.FileName);
            //    currentFile.MoveTo(currentFile.Directory.FullName + "\\" + fileName + currentFile.Extension);

            //    // update location with new filename

            //    location.ImageUrl = fileName + currentFile.Extension;
            //    _context.Entry(location).State = EntityState.Modified;
            //    await _context.SaveChangesAsync();

            //    return CreatedAtAction("GetLocation", new { id = location.LocationId }, location);
            //}

            //return CreatedAtAction("GetLocation", new { id = location.LocationId }, location);
        }

        //PUT for IMAGE update only /uploadImage/{id} of location
        
        [HttpPut(Api.Location.UploadImage)]
        public async Task<ActionResult<Location>> UploadImage(IFormFile file, [FromRoute]int id)
        {
            if (id > 0 && file != null && file.Length > 0)
            {
                if (!Directory.Exists(_environment.WebRootPath + "\\Upload\\"))
                {
                    Directory.CreateDirectory(_environment.WebRootPath + "\\Upload\\");
                }

                using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\Upload\\" + file.FileName))
                {
                    file.CopyTo(fileStream);
                    fileStream.Flush();
                }

                FileInfo currentFile = new FileInfo(_environment.WebRootPath + "\\Upload\\" + file.FileName);
                currentFile.MoveTo(currentFile.Directory.FullName + "\\" + id + currentFile.Extension, true);

                var location = await _context.Location.FirstOrDefaultAsync(l => l.LocationId == id);

                location.ImageUrl = id + currentFile.Extension;
                location.DateEdited = DateTime.Now;

               _context.Entry(location).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                    return Ok(location);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocationExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return NotFound();
        }



        // PUT: api/Locations/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
       
        [HttpPut(Api.Location.EditLocation)]

        public async Task<ActionResult<CreateLocationViewModel>> EditLocation([FromForm]CreateLocationViewModel viewModel, int id)
        //public async Task<ActionResult<Location>> PostLocation([FromBody]CreateLocationViewModel viewModel, IFormFile file)
        {

            var userId = HttpContext.GetUserId();

            var locationFromDb = await _context.Location
                    .Include(l => l.Account.AccountUsers)
                    .Where(l => l.Account.AccountUsers.Any(au => au.ApplicationUserId == userId))
                    .FirstOrDefaultAsync(l => l.LocationId == id);

            if (locationFromDb == null)
            {
                return BadRequest();
            }


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

                //create filname based on created location ID
                int fileName = locationFromDb.LocationId;

                //create path and insert image with original filename

                using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\Upload\\" + viewModel.File.FileName))
                {
                    viewModel.File.CopyTo(fileStream);
                    fileStream.Flush();
                }

                //replace original filename with location ID filename, keeping extension

                FileInfo currentFile = new FileInfo(_environment.WebRootPath + "\\Upload\\" + viewModel.File.FileName);
                currentFile.MoveTo(currentFile.Directory.FullName + "\\" + fileName + currentFile.Extension, true);

                // update location with new filename

                locationFromDb.ImageUrl = fileName + currentFile.Extension;


                
                

                try
                {
                    _context.Entry(locationFromDb).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return BadRequest();
                }
                

                return Ok(locationFromDb);
            }

            

            try 
            {
                _context.Entry(locationFromDb).State = EntityState.Modified;
                await _context.SaveChangesAsync();

            } catch (Exception ex)
            {
                return BadRequest();
            }
            
           

            return Ok(locationFromDb);
        }
       

        [HttpPut(Api.Location.EditLocationIsActive)]
        public async Task<IActionResult> EditLocationIsActive(int id)
        {
            var userId = HttpContext.GetUserId();

            var location = await _context.Location
                    .Include(l => l.Account.AccountUsers)
                    .Where(l => l.Account.AccountUsers.Any(au => au.ApplicationUserId == userId))
                    .FirstOrDefaultAsync(l => l.LocationId == id);




            if (location == null)
            {
                return BadRequest();
            }

            if (location.IsActive == false)
            {
                location.IsActive = true;
            } else
            {
                location.IsActive = false;
            }

            _context.Entry(location).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(location);
        }

        // SOFTDELETE: api/Locations/5

        [HttpDelete(Api.Location.DeleteLocation)]
        public async Task<ActionResult<Location>> DeleteLocation(int id)
        {
            var userId = HttpContext.GetUserId();

            var location = await _context.Location
                    .Include(l => l.Account.AccountUsers)
                    .Where(l => l.Account.AccountUsers.Any(au => au.ApplicationUserId == userId))
                    .FirstOrDefaultAsync(l => l.LocationId == id);

            if (location == null)
            {
                return NotFound();
            }

            location.IsDeleted = true;

            _context.Update(location);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            } 

            return Ok(location);
        }

        //ADD TAGS TO Location
        [HttpPost(Api.Location.AddTag)]
        public async Task<ActionResult<LocationTagResponse>> AddLocationTags(LocationTagRequest request)
        {
            try
            {
                LocationTagResponse response = await _tagService.AddLocationTags(request);
                return Ok(response);
            }
            catch (Exception e)
            {
                
                return NotFound(e);
            }

           
        }

        //REMOVE: TAG FROM LOCATION
      [HttpDelete(Api.Location.DeleteTag)]
        public async Task<ActionResult<LocationTag>> DeleteTag([FromRoute] int locationId, [FromRoute] int tagId)
        {
            var tagToDelete = await _context.LocationTag.FirstOrDefaultAsync(lt => lt.LocationId == locationId && lt.TagId == tagId);
                
            if (tagToDelete == null)
            {
                return NotFound("No tag found");
            }

            _context.LocationTag.Remove(tagToDelete);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return Ok(tagToDelete);
        }

        //ADD CATS TO LOCATION
        [HttpPost(Api.Location.AddCategory)]
        public async Task<ActionResult<LocationCategory>> AddCategory([FromRoute] int locationId, [FromRoute] int categoryId, bool isPrimary)
        {
            try
            {
                var newCat = new LocationCategory()
                {
                    CategoryId = categoryId,
                    LocationId = locationId,
                    IsPrimary = isPrimary
                };

                if (newCat.IsPrimary == true)
                {
                    var catToDelete = await _context.LocationCategory
                        .FirstOrDefaultAsync(lt => lt.LocationId == locationId && lt.IsPrimary == true);

                    if (catToDelete != null)
                    {
                        _context.LocationCategory.Remove(catToDelete);
                        await _context.SaveChangesAsync();
                    }
                }

                _context.LocationCategory.Add(newCat);

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }

                return Ok(newCat);
            }
            catch
            {
                return BadRequest();
            }
        }

        //REMOVE: CATS FROM LOCATION
        [HttpDelete(Api.Location.DeleteCategory)]
        public async Task<ActionResult<LocationCategory>> DeleteCategory([FromRoute] int locationId, [FromRoute] int categoryId)
        {
            var catToDelete = await _context.LocationCategory.FirstOrDefaultAsync(lt => lt.LocationId == locationId && lt.CategoryId == categoryId);

            if (catToDelete == null)
            {
                return NotFound("No Category found");
            }

            _context.LocationCategory.Remove(catToDelete);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return Ok(catToDelete);
        }

        private bool LocationExists(int id)
        {
            return _context.Location.Any(e => e.LocationId == id);
        }
    }
}
