using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Datanomics.Models;
using PagedList;

namespace Web.Datanomics.Controllers
{
    public class ViewController : Controller
    {
        DataContext db = new DataContext();
        //
        // GET: /View/

        //public ActionResult Index()
        //{
        //    using (var db = new DataContext())
        //    {
        //        var model = from p in db.Articles
        //                    select p;

        //        return View(model.ToList());
        //    }
        //}

        public ActionResult Index(int? Page, int CategoriesID = -1)
        {
            int ArticleOnPage = 5;
            if (CategoriesID != -1)
            {
                IQueryable<Article> Article = (from a in db.Articles
                                               where a.CategoryID == CategoriesID
                                               orderby a.ID
                                               select a).AsQueryable();
                var pageNumber = Page ?? 1;
                var onePageOfProducts = Article.ToPagedList(pageNumber, ArticleOnPage);
                ViewBag.OnePageOfProducts = onePageOfProducts;
                return View();
            }
            else
            {
                IQueryable<Article> Article = (from a in db.Articles
                                               orderby a.ID
                                               select a).AsQueryable();
                var pageNumber = Page ?? 1;
                var onePageOfProducts = Article.ToPagedList(pageNumber, ArticleOnPage);
                ViewBag.OnePageOfProducts = onePageOfProducts;
                return View();
            }
            
        }

        public ActionResult Post(int ID = 0)
        {
            using(var db = new DataContext())
            {
                var model = from p in db.Articles
                            where p.ID == ID
                            select p;

                return View(model.ToList());
            }
            
        }

        public ActionResult Whydatanomics()
        {
            return View();
        }

        public ActionResult TechnicalExpertise()
        {
            return View();
        }

        public ActionResult Customers()
        {
            return View();
        }
    }
}
