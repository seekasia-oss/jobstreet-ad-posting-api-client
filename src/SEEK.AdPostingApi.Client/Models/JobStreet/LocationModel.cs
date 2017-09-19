/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
*/
namespace SEEK.AdPostingApi.Client.Models.JobStreet
{
    public class LocationModel
    {
        public LocationModel()
        {
        }

        public LocationModel(LocationModel model)
        {
            this.Id = model.Id;
            this.Area = model.Area;
        }

        public int? Id { get; set; }

        public string Area { get; set; }
    }
}
