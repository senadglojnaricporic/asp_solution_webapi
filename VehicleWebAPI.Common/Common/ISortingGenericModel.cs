using System.Linq;

namespace VehicleWebAPI.Common
{
    public interface ISortingGenericModel<T> where T : class
    {
        string sortBy { get; set; }
        bool sortAscending { get; set; } 

        void Sort(ref IQueryable<T> source);
    }
}