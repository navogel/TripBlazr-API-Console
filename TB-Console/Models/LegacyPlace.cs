using System;

namespace TripBlazrConsole.Models
{
    public class LegacyPlace
    {
        public int Id { get; set; }
        public string PlaceId { get; set; }
        public string YelpId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string VideoUrl { get; set; }
        public int VideoStartTime { get; set; }
        public int VideoEndTime { get; set; }
        public string Tags { get; set; }
        public string City { get; set; }
        public string Summary { get; set; }
        public bool IsLive { get; set; }
        public int MembershipStatus { get; set; }
        public int? SortId { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Website { get; set; }
        public string Hours { get; set; }
        public string ImageUrl { get; set; }
        public DateTime UpdateTimestamp { get; set; }
    }
}