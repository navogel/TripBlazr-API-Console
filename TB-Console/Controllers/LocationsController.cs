using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TripBlazrConsole.Data;
using TripBlazrConsole.Helpers;
using TripBlazrConsole.Models;
using TripBlazrConsole.Models.ViewModels.LocationViewModels;
//using TripBlazrConsole.Routes.V1;

namespace TripBlazrConsole.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        private static IWebHostEnvironment _environment;

        public LocationsController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: api/Locations param city slug
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocationsPublicViewModel>>> GetLocations(string citySlug)
        {
            var applicationDbContext = await _context.Location
               .Include(l => l.Hours)
               .Include(l => l.LocationCategories)
                    .ThenInclude(lc => lc.Category)
               .Include(l => l.LocationTags)
                     .ThenInclude(c => c.Tag)
                .Where(l => l.Account.CitySlug == citySlug)
               .Select(l => new LocationsPublicViewModel()
               {
                   Location = l,
                   Tags = l.LocationTags.Select(t => t.Tag).ToList(),
                   Categories = l.LocationCategories.Select(c => c.Category).ToList(),
                   Hours = l.Hours.ToList()
               }).ToListAsync();


            return Ok(applicationDbContext);
        }

        // GET: api/Locations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Location>> GetLocation(int id)
        {
            var userId = HttpContext.GetUserId();

            var location = await _context.Location
                .Include(l => l.Hours)
                .Include(l => l.LocationCategories)
                    .ThenInclude(lc => lc.Category)
                .Include(l => l.LocationTags)
                    .ThenInclude(lt => lt.Tag)
                .Include(l => l.Account.AccountUsers)
                .Where(l => l.Account.AccountUsers.Any(au => au.ApplicationUserId == userId))

               .FirstOrDefaultAsync(l => l.LocationId == id);



               // .Include(l => l.Account.AccountUsers)
                    //.ThenInclude(au => au.ApplicationUser)
                    //.Where(l => l.Account.AccountUsers.Contains(userId)) 



                


            if (location == null)
            {
                return NotFound();
            }

            return location;
        }

       

        // POST: api/Locations  - CREATE LOCATION
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<Location>> PostLocation([FromForm]CreateLocationViewModel viewModel)
        //public async Task<ActionResult<Location>> PostLocation([FromBody]CreateLocationViewModel viewModel, IFormFile file)
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
                Zipcode = viewModel.Zipcode
            };

           

            //CODE THAT WORKS BELOW

            //if (viewModel.File != null && viewModel.File.Length > 0)
            //{
            //    if (!Directory.Exists(_environment.WebRootPath + "\\Upload\\"))
            //    {
            //        Directory.CreateDirectory(_environment.WebRootPath + "\\Upload\\");
            //    }

            //    using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\Upload\\" + viewModel.File.FileName))
            //    {
            //        viewModel.File.CopyTo(fileStream);
            //        fileStream.Flush();

            //    }
            //    location.ImageUrl = "\\Upload\\" + viewModel.File.FileName;

            //}



           
             _context.Location.Add(location);
            await _context.SaveChangesAsync();
            int id = location.LocationId;



            if (viewModel.File != null && viewModel.File.Length > 0)
            {
                //if (!Directory.Exists(_environment.WebRootPath + "\\Upload\\"))
                //{
                //    Directory.CreateDirectory(_environment.WebRootPath + "\\Upload\\");
                //}
                int fileName = location.LocationId;

               // var ext = Path.GetExtension(viewModel.File);

                using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\Upload\\" + viewModel.File.FileName))
                {
                    viewModel.File.CopyTo(fileStream);
                    fileStream.Flush();
                }

                FileInfo currentFile = new FileInfo(_environment.WebRootPath + "\\Upload\\" + viewModel.File.FileName);
                currentFile.MoveTo(currentFile.Directory.FullName + "\\" + fileName + currentFile.Extension);
                

                location.ImageUrl = "\\Upload\\" + fileName + currentFile.Extension;

                _context.Entry(location).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                

                return Ok(location);
                

            }

            return CreatedAtAction("GetLocation", new { id = location.LocationId }, location);
        }

        //[Route("upload")]
        [AllowAnonymous]
        [HttpPut("upload/{id}")]
        public async Task<ActionResult<Location>> Upload(IFormFile file, [FromRoute]int id)
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

                var location = await _context.Location.FirstOrDefaultAsync(l => l.LocationId == id);

                location.ImageUrl = "\\Upload\\" + file.FileName;


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

                
                //    location.ImageUrl = "\\Upload\\" + file.FileName;

                //bangazonsite version

                //var fileName = Path.GetFileName(location.File.FileName); //getting path of actual file name
                //var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName); //creating path combining file name w/ www.root\\images directory
                //using (var fileSteam = new FileStream(filePath, FileMode.Create)) //using filestream to get the actual path 
                //{
                //    await location.File.CopyToAsync(fileSteam);
                //}
                //location.ImageUrl = fileName;

            }


            return NotFound();
        }



        // PUT: api/Locations/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [AllowAnonymous]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocation(int id, Location location)
        {
            if (id != location.LocationId)
            {
                return BadRequest();
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

            return NoContent();
        }



        // DELETE: api/Locations/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Location>> DeleteLocation(int id)
        {
            var location = await _context.Location.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }

            _context.Location.Remove(location);
            await _context.SaveChangesAsync();

            return location;
        }

        private bool LocationExists(int id)
        {
            return _context.Location.Any(e => e.LocationId == id);
        }
    }
}
