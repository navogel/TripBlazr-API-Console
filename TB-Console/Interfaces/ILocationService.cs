﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripBlazrConsole.Models;
using TripBlazrConsole.Models.ViewModels.LocationViewModels;

namespace TripBlazrConsole.Interfaces
{
    public interface ILocationService
    {
        Task<IEnumerable<LocationViewModel>> GetLocations(string citySlug);

        Task<IEnumerable<LocationViewModel>> GetConsoleLocations(int id, string search, string category, string tag, bool? isActive, string userId);

        Task<LocationViewModel> GetLocation(int id, string userId);

        Task<Location> PostLocation(CreateLocationViewModel viewModel);

        Task<CreateLocationViewModel> EditLocation(CreateLocationViewModel viewModel, int id);

        Task EditLocationIsActive(int id);

        Task<Location> DeleteLocation(int id);


    }
}
