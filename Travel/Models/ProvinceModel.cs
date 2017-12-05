using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Travel.Models
{
    public class Province
    {
        public int ProvinceId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Route> Routes { get; set; }
    }
}