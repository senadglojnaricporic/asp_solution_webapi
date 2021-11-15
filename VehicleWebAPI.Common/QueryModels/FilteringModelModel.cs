using System.Linq;
using VehicleWebAPI.Model;
using VehicleWebAPI.Common;
using Microsoft.EntityFrameworkCore;

namespace VehicleWebAPI.Service
{
    public class FilteringModelModel : IFilteringGenericModel<VehicleModelDataModel>
    {
        public string filterType { get; set; } 
        public string searchString { get; set; }
        public int lowerLimit { get; set; }
        public int upperLimit { get; set; }
        public int limit { get; set; }

        public void Filter(ref IQueryable<VehicleModelDataModel> source)
        {
            switch(filterType)
            {
                case "searchName" :
                    source = source.Where(x => x.Name.Contains(searchString));
                    break;
                case "searchMake" :
                    source = source.Include(x => x.VehicleMake.Name).Where(x => x.VehicleMake.Name.Contains(searchString));
                    break;
                case "searchAbrv" :
                    source = source.Where(x => x.Abrv.Contains(searchString));
                    break;
                case "valueBetweenId" :
                    source = source.Where(x => x.Id > lowerLimit && x.Id < upperLimit);
                    break;
                case "valueLowerThanId" :
                    source = source.Where(x => x.Id < limit);
                    break;
                case "valueGreaterThanId" :
                    source = source.Where(x => x.Id > limit);
                    break;
                default :
                    break;
            }
        }
    }
}