using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Datanomics.Models;

namespace Web.Datanomics.Controllers
{
    
    public class ArticleController : Controller
    {
        private DataContext db = new DataContext();

        //
        // GET: /Article/
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            return View(db.Articles.ToList());
        }

        //
        // GET: /Article/Details/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Details(int id = 0)
        {
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        //
        // GET: /Article/Create
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public ActionResult Create()
        {
            var List = new List<Categories>();
            List = (from c in db.Category
                    orderby c.Order
                    select c).ToList();
            ViewBag.List = List;

            return View();
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Article model)
        {
            string UserName = System.Web.HttpContext.Current.User.Identity.Name;

            int UserID = (from u in db.UserProfiles
                          where u.UserName == UserName
                          select u.ID).FirstOrDefault();

            var Article = new Article();
            Article.CategoryID = model.CategoryID;
            Article.UserID = UserID;
            Article.CreateTime = DateTime.Now;
            Article.Title = model.Title;
            Article.Describe = model.Describe;
            Article.Image = model.Image;
            Article.Content = model.Content;

            if (ModelState.IsValid)
            {
                db.Articles.Add(Article);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }


        //
        // GET: /Article/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id = 0)
        {
            

            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }

            var List = new List<Categories>();
            List = (from c in db.Category
                    orderby c.Order
                    select c).ToList();
            ViewBag.List = List;

            return View(article);
        }

        //
        // POST: /Article/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(Article article)
        {
            string UserName = System.Web.HttpContext.Current.User.Identity.Name;
            int UserID = (from u in db.UserProfiles
                          where u.UserName == UserName
                          select u.ID).FirstOrDefault();

            article.UserID = UserID;
            article.CreateTime = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(article);
        }

        //
        // GET: /Article/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id = 0)
        {
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        //
        // POST: /Article/Delete/5
        [Authorize(Roles = "Administrator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = db.Articles.Find(id);
            db.Articles.Remove(article);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}