using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VehicleWebAPI.Model;
using VehicleWebAPI.Common;

namespace VehicleWebAPI.Service
{
    public class PagingModelModel : IPagingGenericModel<VehicleModelDataModel>
    {
        public int pageIndex { get; set; }
        public int pageSize { get; set; }

        public async Task<List<VehicleModelDataModel>> PageAsync(IQueryable<VehicleModelDataModel> source)
        {
            return await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        }
    }
}