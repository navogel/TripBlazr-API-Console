using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripBlazrConsole.Data;
using TripBlazrConsole.Interfaces;
using TripBlazrConsole.Models;

namespace TripBlazrConsole.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<LocationCategory> AddCategory(int locationId, int categoryId, bool isPrimary)
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

            return newCat;
            
        }

        public async Task<LocationCategory> DeleteCategory(int locationId, int categoryId)
        {
            var catToDelete = await _context.LocationCategory.FirstOrDefaultAsync(lt => lt.LocationId == locationId && lt.CategoryId == categoryId);

            if (catToDelete == null)
            {
                return null;
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

            return catToDelete;
        }
    }
}
