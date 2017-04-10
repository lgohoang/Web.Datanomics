using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Datanomics.Models;

namespace Web.Datanomics.Controllers
{
    public class HomeController : Controller
    {
        DataContext db = new DataContext();

        [OutputCache(Duration = 10)]
        public ActionResult Index()
        {
            ///Slider

            var Slider = (from sl in db.Articles
                          join ca in db.Category
                          on sl.CategoryID equals ca.ID
                          where ca.ID == 1
                          orderby sl.ID
                          select sl).Take(4);

            ViewBag.Slider = Slider.ToList();



            ///Solution Areas
            var solution_areas = (from sa in db.Articles
                                 join ca in db.Category
                                 on sa.CategoryID equals ca.ID
                                 where ca.ID == 2
                                 orderby sa.ID
                                 select sa).Take(8);
            var NumberOfRow = 4;
            var Count = solution_areas.Count();
            var NumberRow = Count / NumberOfRow + ((Count % NumberOfRow == 0 ? 0 : 1));

            ViewBag.NumberOfRow = NumberOfRow;
            ViewBag.Count = Count;
            ViewBag.NumberRow = NumberRow;

            ViewBag.Solution_Areas = solution_areas.ToList();

            ///Lasted Posts
            ///
            var lasted_post = (from lp in db.Articles
                               orderby lp.ID ascending
                               select lp).Take(5);
            ViewBag.LastedPost = lasted_post.ToList();


            ///Our Clients
            ///
            var our_client = from o in db.OurClients
                             select o;

            ViewBag.OurClients = our_client.ToList();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Cms()
        {
            ViewBag.Message = "Coming Soon Page.";
            return View();
        }
    }
}
