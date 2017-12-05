using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
namespace Travel.Models
{
    public class Trips
    {
        public Route Trip { get; set; }
        public string DriverId { get; set; }
        public List<ApplicationUser> UsersList { get; set; }
    }

    public class TripsViewModel
    {
        //public List<Trips> TripsList { get; set; }
        public IPagedList<Models.Trips> TripsList { get; set; }
        public ApplicationUser User { get; set; }
    }
}