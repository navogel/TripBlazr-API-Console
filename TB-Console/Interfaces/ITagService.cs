using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripBlazrConsole.Models;

namespace TripBlazrConsole.Interfaces
{
    public interface ITagService
    {
        Task<LocationTagResponse> AddLocationTags(LocationTagRequest request);

        Task<LocationTag> DeleteTag(int locationId, int tagId);
    }
}
