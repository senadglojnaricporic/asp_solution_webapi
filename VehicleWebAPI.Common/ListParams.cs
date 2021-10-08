using System;

namespace VehicleWebAPI.Common
{
    public class ListParams
    {
        public string sortOrder { get; set; }
        public string searchString { get; set; }
        public int listStart { get; set; }
        public int listSize { get; set; }
    }
}
