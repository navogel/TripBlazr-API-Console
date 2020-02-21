using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TripBlazrConsole.Routes.V1
{
    public static class Api
    {
        internal const string Root = "api";
        internal const string Version = "v1";
        internal const string Base = Root + "/" + Version;

        public static class Values
        {
            public const string GetAll = Base + "/Values";
            public const string Get = Base + "/Values/{id}";
        }

        public static class User
        {
            public const string Login = Base + "/Auth/Login";
            public const string Register = Base + "/Auth/Register";
            public const string Refresh = Base + "/Auth/Refresh";
        }

        public static class Location
        {
            public const string GetLocations = Base + "/Locations/City/{citySlug}";
            public const string GetConsoleLocations = Base + "/Locations/ByAccount/{id}";
            public const string GetLocation = Base + "/Locations/{id}";
            public const string PostLocation = Base + "/Locations";
            public const string EditLocation = Base + "/Locations/{id}";
            public const string EditLocationIsActive = Base + "/Locations/{id}/isActive";
            public const string DeleteLocation = Base + "/Locations/{id}";
            public const string AddTag = Base + "/Locations/AddTags";
            public const string DeleteTag = Base + "/Locations/{locationId}/DeleteTag/{tagId}";
            public const string AddCategory = Base + "/Locations/{locationId}/AddCategory/{categoryId}";
            public const string DeleteCategory = Base + "/Locations/{locationId}/DeleteCategory/{categoryId}";
        }
    }
}
