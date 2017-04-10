using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Web.Datanomics.Models;

namespace Web.Datanomics.Controllers
{
    public class ClientController : Controller
    {
        private DataContext db = new DataContext();

        //
        // GET: /Client/
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            return View(db.OurClients.ToList());
        }

        //
        // GET: /Client/Details/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Details(int id = 0)
        {
            OurClients ourclients = db.OurClients.Find(id);
            if (ourclients == null)
            {
                return HttpNotFound();
            }
            return View(ourclients);
        }

        //
        // GET: /Client/Create

        [Authorize(Roles="Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Client/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create(OurClients ourclients)
        {
            if (ModelState.IsValid)
            {
                db.OurClients.Add(ourclients);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ourclients);
        }

        //
        // GET: /Client/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id = 0)
        {
            OurClients ourclients = db.OurClients.Find(id);
            if (ourclients == null)
            {
                return HttpNotFound();
            }
            return View(ourclients);
        }

        //
        // POST: /Client/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(OurClients ourclients)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ourclients).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ourclients);
        }

        //
        // GET: /Client/Delete/5

        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id = 0)
        {
            OurClients ourclients = db.OurClients.Find(id);
            if (ourclients == null)
            {
                return HttpNotFound();
            }
            return View(ourclients);
        }

        //
        // POST: /Client/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            OurClients ourclients = db.OurClients.Find(id);
            db.OurClients.Remove(ourclients);
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