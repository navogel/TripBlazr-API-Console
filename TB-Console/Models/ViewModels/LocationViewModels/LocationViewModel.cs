using System.Collections.Generic;

namespace TripBlazrConsole.Models.ViewModels.LocationViewModels
{
    public class LocationViewModel
    {
        public int LocationId { get; set; }
        public int AccountId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Website { get; set; }
        public string ShortSummary { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int? SortId { get; set; }
        public string VideoId { get; set; }
        public int? VideoStartTime { get; set; }
        public int? VideoEndTime { get; set; }
        public string ImageUrl { get; set; }
        public bool? IsActive { get; set; } 
        public bool? IsDeleted { get; set; } 
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int? Zipcode { get; set; }
        public bool? SeeWebsite { get; set; }
        public string HoursNotes { get; set; }
        public List<Tag> Tags { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
        public List<Hours> Hours { get; set; }
    }
}