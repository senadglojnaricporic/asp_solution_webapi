using System.Linq;
using VehicleWebAPI.Model;
using VehicleWebAPI.Common;
using Microsoft.EntityFrameworkCore;

namespace VehicleWebAPI.Service
{
    public class SortingModelModel : ISortingGenericModel<VehicleModelDataModel>
    {
        public string sortBy { get; set; }
        public bool sortAscending { get; set; } = true;

        public void Sort(ref IQueryable<VehicleModelDataModel> source)
        {
            if(sortAscending)
            {
                switch(sortBy)
                {
                    case "name" :
                        source = source.OrderBy(x => x.Name);
                        break;
                    case "make" :
                        source = source.Include(x => x.VehicleMake.Name).OrderBy(x => x.VehicleMake.Name);
                        break;
                    case "abrv" :
                        source = source.OrderBy(x => x.Abrv);
                        break;
                    default :
                        source = source.OrderBy(x => x.Id);
                        break;
                }
            }
            else
            {
                switch(sortBy)
                {
                    case "name" :
                        source = source.OrderByDescending(x => x.Name);
                        break;
                    case "make" :
                        source = source.Include(x => x.VehicleMake.Name).OrderByDescending(x => x.VehicleMake.Name);
                        break;
                    case "abrv" :
                        source = source.OrderByDescending(x => x.Abrv);
                        break;
                    default :
                        source = source.OrderByDescending(x => x.Id);
                        break;
                }
            }
        }
    }
}