using VehicleWebAPI.Service.Common;

namespace VehicleWebAPI.WebAPI
{
    public class QueryModel : IQueryModel
    {
        public string sortBy { get; set; }
        public bool sortAscending { get; set; }
        public string filterType { get; set; }
        public string searchString { get; set; }
        public int lowerLimit { get; set; }
        public int upperLimit { get; set; }
        public int limit { get; set; }
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
    }
}