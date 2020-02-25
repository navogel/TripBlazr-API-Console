using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripBlazrConsole.Models;

namespace TripBlazrConsole.Interfaces
{
    public interface ICategoryService
    {
        Task<LocationCategory> AddCategory(int locationId, int categoryId, bool isPrimary);

        Task<LocationCategory> DeleteCategory(int locationId, int categoryId);
    }
}

