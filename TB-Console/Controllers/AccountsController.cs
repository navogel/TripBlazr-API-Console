using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TripBlazrConsole.Data;
using TripBlazrConsole.Helpers;
using TripBlazrConsole.Models;
using TripBlazrConsole.Models.ViewModels.ConsoleViewModels;

namespace TripBlazrConsole.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AccountsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Accounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountViewModel>>> GetAccounts()
        {
            try
            {
                var userId = HttpContext.GetUserId();

                var accounts = await _context.Account
                    .Include(a => a.AccountUsers)
                    .Where(a => a.AccountUsers.Any(au => au.ApplicationUserId == userId))
                    .Select(a => new AccountViewModel()
                    {
                        AccountId = a.AccountId,
                        Name = a.Name,
                        City = a.City,
                        Latitude = a.Latitude,
                        Longitude = a.Longitude
                    }).ToListAsync();

                return Ok(accounts);
                    
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: api/Accounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountViewModel>> GetAccount(int id)
        {
            try
            {
                var userId = HttpContext.GetUserId();

                var account = await _context.Account
                        .Include(a => a.AccountUsers)

                        .Where(a => a.AccountUsers.Any(au => au.ApplicationUserId == userId))
                        .FirstOrDefaultAsync(a => a.AccountId == id);

                var accountToSend = new AccountViewModel()
                {
                    AccountId = account.AccountId,
                    Name = account.Name,
                    City = account.City,
                    Latitude = account.Latitude,
                    Longitude = account.Longitude
                };

                // var account = await _context.Account.FindAsync(id);

                if (account == null)
                {
                    return NotFound();
                }

                return Ok(accountToSend);
            } catch
            {
                return BadRequest();
            }
        }

        // PUT: api/Accounts/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(int id, Account account)
        {
            if (id != account.AccountId)
            {
                return BadRequest();
            }

            _context.Entry(account).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(id))
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

        // POST: api/Accounts
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Account>> PostAccount(Account account)
        {
            _context.Account.Add(account);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAccount", new { id = account.AccountId }, account);
        }

        // DELETE: api/Accounts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Account>> DeleteAccount(int id)
        {
            var account = await _context.Account.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            _context.Account.Remove(account);
            await _context.SaveChangesAsync();

            return account;
        }

        private bool AccountExists(int id)
        {
            return _context.Account.Any(e => e.AccountId == id);
        }
    }
}
