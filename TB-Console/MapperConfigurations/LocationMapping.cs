using AutoMapper;
using TripBlazrConsole.Models;
using TripBlazrConsole.Models.ViewModels.LocationViewModels;

namespace TripBlazrConsole.MapperConfigurations
{
    public class LocationMapping: Profile
    {
        public LocationMapping()
        {
            CreateMap<Location, LocationViewModel>()
                .ForMember(lvm => lvm.Tags, opt => opt
                    .MapFrom(l => l.LocationTags))
                .ForMember(lvm => lvm.Hours, opt => opt
                    .MapFrom(l => l.Hours))
                .ForMember(lvm => lvm.Categories, opt => opt
                    .MapFrom(l => l.LocationCategories));

            CreateMap<LocationTag, Tag>()
                .ForMember(t => t.Name, opt => opt.MapFrom(lt => lt.Tag.Name))
                .ForMember(t => t.Image, opt => opt.MapFrom(lt => lt.Tag.Image));

            CreateMap<LocationCategory, CategoryViewModel>()
                .ForMember(c => c.Name, opt => opt.MapFrom(lc => lc.Category.Name))
                .ForMember(c => c.Image, opt => opt.MapFrom(lc => lc.Category.Image))
                .ForMember(c => c.IsPrimary, opt => opt.MapFrom(lc => lc.IsPrimary));
        }
    }
}