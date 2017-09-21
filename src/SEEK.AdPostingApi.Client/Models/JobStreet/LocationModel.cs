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
            this.id = model.id;
            this.area = model.area;
        }

        public int?[] id { get; set; }

        public string area { get; set; }
    }
}
