using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Travel.Models;
using Microsoft.AspNet.Identity;
using PagedList;
namespace Travel.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<Province> provinces;
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                provinces = dbContext.Provinces.ToList();
            }
            return View(provinces);
        }

        public ActionResult Trips(int id, int? page)
        {
            TripsViewModel model;
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                var data = from route in dbContext.Routes where route.ProvinceId == id orderby route.RouteId select new Trips { Trip = route, DriverId= route.DriverId, UsersList = route.Users.ToList() };
                
                int pageSize = 5;
                int pageNumber = (page ?? 1);
                
                model = new TripsViewModel { TripsList = data.ToPagedList(pageNumber, pageSize), User = UserInstance(dbContext) };
            }
            ViewBag.Id = id;
            return View(model);
        }

        public ActionResult TripDetails(int id, int? page)
        {
            TravelDetailsViewModel model;
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                Route route = dbContext.Routes.SingleOrDefault<Route>(a => a.RouteId == id);
                var comments = from comment in dbContext.Comments where comment.RouteId == id orderby comment.CommentId select comment;
                int pageSize = 5;
                int pageNumber = (page ?? 1);
                model = new TravelDetailsViewModel { Route = route, Users = route.Users.ToList(), DriverId = route.DriverId, User = UserInstance(dbContext), Comments= comments.ToPagedList(pageNumber,pageSize) };
            }
            ViewBag.Id = id;
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddComment(int id, string comment)
        {
            Comment commentInstance = new Comment { Body = comment, RouteId = id };
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                dbContext.Comments.Add(commentInstance);
                dbContext.SaveChanges();
            }
                return RedirectToAction("TripDetails", new { id = id });
        }

        [Authorize]
        public ActionResult AddPassenger(int id)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                Route route = dbContext.Routes.SingleOrDefault<Route>(a => a.RouteId == id);
                ApplicationUser user = UserInstance(dbContext);
                if(route.Users.Contains(user))
                {
                    ViewBag.Message = "Jesteś uczestnikiem poróży";
                    return View();
                }
                if (route.Users.Count >= route.MaxPassengerCount)
                {
                    ViewBag.Message = "Brak wolnych miejsc";
                    return View();
                }
                route.Users.Add(user);
                dbContext.SaveChanges();
                ViewBag.Message = "Dołączyłeś do podróży";
            }
            return View();
        }


        [Authorize]
        public ActionResult DeletePassenger(int id)
        {

            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                Route route = dbContext.Routes.SingleOrDefault<Route>(a => a.RouteId == id);
                route.Users.Remove(UserInstance(dbContext));
                dbContext.SaveChanges();
                ViewBag.Message = "Zrezygnowałeś z podróży";
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private ApplicationUser UserInstance(ApplicationDbContext dbContext)
        {
            string userId = User.Identity.GetUserId();
            return dbContext.Users.SingleOrDefault<ApplicationUser>(a => a.Id == userId);
        }
    }
}