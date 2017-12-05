using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Travel.Models;

namespace Travel.Controllers
{
    public class MyTripsController : Controller
    {
        // GET: MyTrips
        [Authorize]
        public ActionResult Index()
        {
            List<Route> routes;
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                string userId = User.Identity.GetUserId();
                ViewBag.UserId = userId;
                routes = (from r in dbContext.Routes where r.Users.Any(u => u.Id==userId) || r.DriverId == userId  select r).ToList();
            }

            return View(routes);
        }

        public ActionResult AddTrip()
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                ViewBag.ProvinceId = new SelectList(dbContext.Provinces.ToList(), "ProvinceId", "Name");
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddTrip(Route route)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                dbContext.Routes.Add(route);
                route.DriverId = User.Identity.GetUserId();
                dbContext.SaveChanges();
            }
                return RedirectToAction("TripDetails", "Home", new { id = route.RouteId });
        }

        public ActionResult DeleteTrip(int id)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                Route currentRoute = dbContext.Routes.SingleOrDefault(r => r.RouteId == id);
                if (currentRoute != null)
                {
                    currentRoute.Users.Clear();
                    dbContext.Routes.Remove(currentRoute);
                    dbContext.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        private ApplicationUser UserInstance(ApplicationDbContext dbContext)
        {
            string userId = User.Identity.GetUserId();
            return dbContext.Users.SingleOrDefault<ApplicationUser>(a => a.Id == userId);
        }
    }
}