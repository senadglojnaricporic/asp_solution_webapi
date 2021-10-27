using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleWebAPI.Common
{
    public interface IPagingGenericModel<T>
    {
        int pageIndex { get; set; }
        int pageSize { get; set; }

        Task<List<T>> PageAsync(IQueryable<T> source);
    }
}