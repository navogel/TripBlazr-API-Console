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
        private readonly ITagService _tagService;

        private readonly ILocationService _locationService;

        private readonly ICategoryService _categoryService;

        public LocationsController(ITagService tagService, ILocationService locationService, ICategoryService categoryService)
        {
            _tagService = tagService;
            _locationService = locationService;
            _categoryService = categoryService;
        }

        // GET: CLIENT: ANON: api/Locations/citySlug
        [AllowAnonymous]
        [HttpGet(Api.Location.GetLocations)]
        public async Task<ActionResult<IEnumerable<LocationViewModel>>> GetLocations(string citySlug)
        {
            var response = await _locationService.GetLocations(citySlug);

            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);  
        }

        // GET: CONSOLE: PRIVATE: api/Locations/Account/{id}?search/category/tag={params}
        
        [HttpGet(Api.Location.GetConsoleLocations)]
        public async Task<ActionResult<IEnumerable<LocationViewModel>>> GetConsoleLocations(int id, string search, string category, string tag, bool? isActive)
        {
            
            var userId = HttpContext.GetUserId();

            var response = await _locationService.GetConsoleLocations(id, search, category, tag, isActive, userId);

            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        // GET: CONSOLE: PRIVATE api/Locations/by Id
       
        [HttpGet(Api.Location.GetLocation)]
        public async Task<ActionResult<LocationViewModel>> GetLocation(int id)
        {
            var userId = HttpContext.GetUserId();

            var response = await _locationService.GetLocation(id, userId);

            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
            }

        // POST: api/Locations  - CREATE LOCATION
        
       
        [HttpPost(Api.Location.PostLocation)]
        public async Task<ActionResult<Location>> PostLocation([FromForm]CreateLocationViewModel viewModel)
        {
            var response = await _locationService.PostLocation(viewModel);

            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        // PUT: api/Locations/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
       
        [HttpPut(Api.Location.EditLocation)]

        public async Task<ActionResult<Location>> EditLocation([FromForm]CreateLocationViewModel viewModel, int id)
        {
            var userId = HttpContext.GetUserId();

            var response = await _locationService.EditLocation(viewModel, id, userId);

            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }
       

        [HttpPut(Api.Location.EditLocationIsActive)]
        public async Task<IActionResult> EditLocationIsActive(int id)
        {
            var userId = HttpContext.GetUserId();
           
            var response = await _locationService.EditLocationIsActive (id, userId);

            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        // SOFTDELETE: api/Locations/5

        [HttpDelete(Api.Location.DeleteLocation)]
        public async Task<ActionResult<Location>> DeleteLocation(int id)
        {
            var userId = HttpContext.GetUserId();
            
            var response = await _locationService.DeleteLocation(id, userId);

            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        //ADD TAGS TO Location
        [HttpPost(Api.Location.AddTag)]
        public async Task<ActionResult<LocationTagResponse>> AddLocationTags(LocationTagRequest request)
        {
            LocationTagResponse response = await _tagService.AddLocationTags(request);
                
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        //REMOVE: TAG FROM LOCATION
      [HttpDelete(Api.Location.DeleteTag)]
        public async Task<ActionResult<LocationTag>> DeleteTag([FromRoute] int locationId, [FromRoute] int tagId)
        {
            var response = await _tagService.DeleteTag(locationId, tagId);
            
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        //ADD CATS TO LOCATION
        [HttpPost(Api.Location.AddCategory)]
        public async Task<ActionResult<LocationCategory>> AddCategory([FromRoute] int locationId, [FromRoute] int categoryId, bool isPrimary)
        {
            var response = await _categoryService.AddCategory(locationId, categoryId, isPrimary);
                
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        //REMOVE: CATS FROM LOCATION
        [HttpDelete(Api.Location.DeleteCategory)]
        public async Task<ActionResult<LocationCategory>> DeleteCategory([FromRoute] int locationId, [FromRoute] int categoryId)
        {
            var response = await _categoryService.DeleteCategory(locationId, categoryId);
                
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

    }
}
