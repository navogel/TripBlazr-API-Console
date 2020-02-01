using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TripBlazrConsole.Data;
using TripBlazrConsole.Models;
using TripBlazrConsole.Models.ViewModels.TagViewModels;

namespace TripBlazrConsole.Controllers
{
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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuTagsViewModel>>> GetMenuGroup(string citySlug)
        {
            

               var applicationDbContext = await _context.MenuGroup

               .Include(m => m.TagMenuGroups)
                    .ThenInclude(mg => mg.Tag)             
               .OrderBy(m => m.SortId)
               .Where(m => m.Account.CitySlug == citySlug)


               .Select(l => new MenuTagsViewModel()
               {
                   MenuGroupId = l.MenuGroupId,
                   Name = l.Name,
                   SortId = l.SortId,
                   Tags = l.TagMenuGroups.Select(t => t.Tag).ToList(),

               }).ToListAsync();


            return Ok(applicationDbContext);
        }

        // GET: api/MenuGroups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MenuGroup>> GetMenuGroup(int id)
        {
            var menuGroup = await _context.MenuGroup.FindAsync(id);

            if (menuGroup == null)
            {
                return NotFound();
            }

            return menuGroup;
        }

        // PUT: api/MenuGroups/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMenuGroup(int id, MenuGroup menuGroup)
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

        // POST: api/MenuGroups
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<MenuGroup>> PostMenuGroup(MenuGroup menuGroup)
        {
            _context.MenuGroup.Add(menuGroup);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMenuGroup", new { id = menuGroup.MenuGroupId }, menuGroup);
        }

        // DELETE: api/MenuGroups/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MenuGroup>> DeleteMenuGroup(int id)
        {
            var menuGroup = await _context.MenuGroup.FindAsync(id);
            if (menuGroup == null)
            {
                return NotFound();
            }

            _context.MenuGroup.Remove(menuGroup);
            await _context.SaveChangesAsync();

            return menuGroup;
        }

        private bool MenuGroupExists(int id)
        {
            return _context.MenuGroup.Any(e => e.MenuGroupId == id);
        }
    }
}
