using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VehicleWebAPI.Model;
using VehicleWebAPI.Service.Common;

namespace VehicleWebAPI.Service
{
    public class PagingMakeModel : IPagingGenericModel<VehicleMakeDataModel>
    {
        public int pageIndex { get; set; }
        public int pageSize { get; set; }

        public async Task<List<VehicleMakeDataModel>> PageAsync(IQueryable<VehicleMakeDataModel> source)
        {
            return await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        }
    }
}