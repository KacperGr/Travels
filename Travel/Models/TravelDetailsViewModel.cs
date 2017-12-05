using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Travel.Models
{
    public class TravelDetailsViewModel
    {
        public Route Route { get; set; }
        public List<ApplicationUser> Users { get; set; }
        public string DriverId { get; set; }
        public ApplicationUser User { get; set; }
        public IPagedList<Models.Comment> Comments { get; set; }
        //public List<string> Comments { get; set; }
    }
}