﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TripBlazrConsole.Data;
using TripBlazrConsole.Models;
using TripBlazrConsole.Models.ViewModels.TagViewModels;

namespace TripBlazrConsole.Controllers
{
    // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MenuGroupsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MenuGroupsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/MenuGroups
        [AllowAnonymous]
        [HttpGet("city/{citySlug}")]
        public async Task<ActionResult<IEnumerable<MenuTagsDetailViewModel>>> GetMenuGroup(string citySlug)
        {
            
               var menuGroup = await _context.MenuGroup
               .Include(m => m.TagMenuGroups)
                    .ThenInclude(mg => mg.Tag)             
               .OrderBy(m => m.SortId)
               .Where(m => m.Account.CitySlug == citySlug)
               .Select(l => new MenuTagsDetailViewModel()
               {
                   MenuGroupId = l.MenuGroupId,
                   Name = l.Name,
                   SortId = l.SortId,
                   Tags = l.TagMenuGroups.Select(t => t.Tag).ToList(),
               }).ToListAsync();

            return Ok(menuGroup);
        }

        // GET: api/MenuGroups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MenuTagsDetailsViewModel>> GetMenuGroup(int id)
        {
            var menuGroup = await _context.MenuGroup.FindAsync(id);

            if (menuGroup == null)
            {
                return NotFound();
            }

            return Ok(menuGroup);
        }



        // POST: api/MenuGroups
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
       // [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<MenuTagsDetailsViewModel>> PostMenuGroup(MenuTagsDetailsViewModel menuGroup)
        {
            _context.MenuGroup.Add(menuGroup);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMenuGroup", new { id = menuGroup.MenuGroupId }, menuGroup);
        }

       


        // PUT: api/MenuGroups/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMenuGroup(int id, MenuTagsDetailsViewModel menuGroup)
        {
            if (id != menuGroup.MenuGroupId)
            {
                return BadRequest();
            }

            _context.Entry(menuGroup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuGroupExists(id))
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


        // DELETE: api/MenuGroups/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MenuTagsDetailsViewModel>> DeleteMenuGroup(int id)
        {
            var menuGroup = await _context.MenuGroup.FindAsync(id);

            if (menuGroup == null)
            {
                return NotFound();
            }

            _context.MenuGroup.Remove(menuGroup);

            await _context.SaveChangesAsync();

            return new StatusCodeResult(StatusCodes.Status204NoContent);
        }

        //CREATE: ADD TAGS TO MENU GROUP
        [HttpPost("{menuGroupId}/AddTag/{tagId}")]
        public async Task<ActionResult<TagMenuGroup>> AddTag([FromRoute]int menuGroupId,[FromRoute] int tagId)
        {
            try { 
                var newTag = new TagMenuGroup()
                {
                    TagId = tagId,
                    MenuGroupId = menuGroupId
                };

                _context.TagMenuGroup.Add(newTag);

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }

                return Ok(newTag);

            }catch
            {
                return BadRequest();
            }
}

        // REMOVE: TAG FROM MENU GROUP 
        [HttpDelete("{menuGroupId}/RemoveTag/{tagId}")]
        public async Task<ActionResult<TagMenuGroup>> DeleteTag([FromRoute]int menuGroupId, [FromRoute]int tagId)
        {
            var tagToDelete = await _context.TagMenuGroup.FirstOrDefaultAsync(tmg => tmg.MenuGroupId == menuGroupId && tmg.TagId == tagId);
            
            if (tagToDelete == null)
            {
                return NotFound("No tag found");
            }

            _context.TagMenuGroup.Remove(tagToDelete);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return new StatusCodeResult(StatusCodes.Status204NoContent);
        }

        //HELPER FUNCTIONS

        private bool MenuGroupExists(int id)
        {
            return _context.MenuGroup.Any(e => e.MenuGroupId == id);
        }
    }
}
