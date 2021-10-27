using System.Linq;
using VehicleWebAPI.Model;
using VehicleWebAPI.Common;

namespace VehicleWebAPI.Service
{
    public class FilteringMakeModel : IFilteringGenericModel<VehicleMakeDataModel>
    {
        public string filterType { get; set; } 
        public string searchString { get; set; }
        public int lowerLimit { get; set; }
        public int upperLimit { get; set; }
        public int limit { get; set; }

        public void Filter(ref IQueryable<VehicleMakeDataModel> source)
        {
            switch(filterType)
            {
                case "searchName" :
                    source = source.Where(x => x.Name.Contains(searchString));
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