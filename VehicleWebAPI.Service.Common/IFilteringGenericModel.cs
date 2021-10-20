using System.Linq;

namespace VehicleWebAPI.Service.Common
{
    public interface IFilteringGenericModel<T> where T : class
    {
        string filterType { get; set; }
        string searchString { get; set; }
        int lowerLimit { get; set; }
        int upperLimit { get; set; }
        int limit { get; set; }

        void Filter(ref IQueryable<T> source);
    }
}