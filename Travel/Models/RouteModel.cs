using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Travel.Models
{
    [Bind(Exclude ="RouteId")]
    public class Route
    {
        public Route()
        {
            Users = new HashSet<ApplicationUser>();
        }
        public int RouteId { get; set; }
        [DisplayName("Punkt początkowy")]
        public string FromName { get; set; }
        [DisplayName("Punkt docelowy")]
        public string ToName { get; set; }
        [DisplayName("Data")]
        public DateTime TravelDate { get; set; }
        [DisplayName("Województwo")]
        public int ProvinceId { get; set; }
        [DisplayName("Liczba pasażerów")]
        public int MaxPassengerCount { get; set; }
        [DisplayName("Kierowca")]
        public string DriverId { get; set; }
        [DisplayName("Opis podróży")]
        public string Description { get; set; }

        public virtual ApplicationUser Driver { get; set; }
        public virtual Province Province { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}