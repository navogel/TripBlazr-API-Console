using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TripBlazrConsole.Data;
using TripBlazrConsole.Models;
using TripBlazrConsole.Models.ViewModels.HoursViewModels;

namespace TripBlazrConsole.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoursController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HoursController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Hours
        [HttpGet("ByLocation/{id}")]
        public async Task<ActionResult<IEnumerable<Hours>>> GetHoursByLocation(int id)
        {
            return await _context.Hours
                .Where(h => h.LocationId == id)
                .ToListAsync();
        }

        // GET: api/Hours/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hours>> GetHours(int id)
        {
            var hours = await _context.Hours.FindAsync(id);

            if (hours == null)
            {
                return NotFound();
            }

            return hours;
        }

        // POST: api/Hours
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<HoursViewModel>> PostHours(HoursViewModel viewModel)
        {
            try
            {
                var hours = new Hours()
                {
                    LocationId = viewModel.LocationId,
                    DayCode = viewModel.DayCode,
                    Open = viewModel.Open,
                    Close = viewModel.Close
                };
                _context.Hours.Add(hours);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetHours", new { id = hours.HoursId }, hours);
            }
            catch (Exception ex)
            {
                return BadRequest("bad request");
            }

        }

        // PUT: api/Hours/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHours(int id, Hours hours)
        {
            if (id != hours.HoursId)
            {
                return BadRequest();
            }

            _context.Entry(hours).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HoursExists(id))
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

        

        // DELETE: api/Hours/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Hours>> DeleteHours(int id)
        {
            var hours = await _context.Hours.FindAsync(id);
            if (hours == null)
            {
                return NotFound();
            }

            _context.Hours.Remove(hours);
            await _context.SaveChangesAsync();

            return hours;
        }

        private bool HoursExists(int id)
        {
            return _context.Hours.Any(e => e.HoursId == id);
        }
    }
}
