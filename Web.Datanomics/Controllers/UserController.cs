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
    public class UserController : Controller
    {
        private DataContext db = new DataContext();

        //
        // GET: /User/

        public ActionResult Index()
        {
            return View(db.UserProfiles.ToList());
        }

        //
        // GET: /User/Details/5

        public ActionResult Details(int id = 0)
        {
            UserProfile user = db.UserProfiles.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // GET: /User/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /User/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserProfile user)
        {
            if (ModelState.IsValid)
            {
                db.UserProfiles.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        //
        // GET: /User/Edit/5

        public ActionResult Edit(int id = 0)
        {
            UserProfile user = db.UserProfiles.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserProfile user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        //
        // GET: /User/Delete/5

        public ActionResult Delete(int id = 0)
        {
            UserProfile user = db.UserProfiles.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // POST: /User/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserProfile user = db.UserProfiles.Find(id);
            db.UserProfiles.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult GetFullName(int? id)
        {
            if(id == null){
                string temp = "";
                return PartialView((object)temp);
            }
            string FullName = "";
            try
            {
                 FullName = (from m in db.UserProfiles
                                   where m.ID == id
                                   select (m.FullName != "")? m.FullName : m.UserName).FirstOrDefault().ToString();
            }
            catch
            {
                FullName = "Null";
            }

            return PartialView((object)FullName);

        }
    }
}