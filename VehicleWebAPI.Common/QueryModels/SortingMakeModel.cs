using System.Linq;
using VehicleWebAPI.Model;
using VehicleWebAPI.Common;

namespace VehicleWebAPI.Service
{
    public class SortingMakeModel : ISortingGenericModel<VehicleMakeDataModel>
    {
        public string sortBy { get; set; }
        public bool sortAscending { get; set; } = true;

        public void Sort(ref IQueryable<VehicleMakeDataModel> source)
        {
            if(sortAscending)
            {
                switch(sortBy)
                {
                    case "name" :
                        source = source.OrderBy(x => x.Name);
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