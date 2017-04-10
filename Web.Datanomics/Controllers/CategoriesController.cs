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
    
    public class CategoriesController : Controller
    {
        private DataContext db = new DataContext();


        //public ActionResult CategoryName(int ID)
        //{

        //}
        //
        // GET: /Categories/

        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            return View(db.Category.ToList());
        }

        //
        // GET: /Categories/Details/5

        [Authorize(Roles = "Administrator")]
        public ActionResult Details(int id = 0)
        {
            Categories categories = db.Category.Find(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }

        //
        // GET: /Categories/Create

        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            using (var db = new DataContext())
            {
                List<SelectListItem> parent_id = new List<SelectListItem>();



                var item_parent_id = (from c in db.Category
                             where c.isParent == true
                             orderby c.Order
                             select c).ToList().Select(u => new SelectListItem
                             {
                                 Text = u.Name,
                                 Value = u.ID.ToString()
                             });

                parent_id.Add(new SelectListItem { Text = "isParent", Value = "0", Selected = true });
                parent_id.AddRange(item_parent_id);
                ViewBag.Parent_Id = parent_id;

                List<SelectListItem> menu_id = new List<SelectListItem>();
                var item_menu_id = (from c in db.Menu
                             where c.isGroup == false
                             orderby c.Order
                             select c).ToList().Select(u => new SelectListItem
                             {
                                 Text = u.Name,
                                 Value = u.ID.ToString()
                             });

                menu_id.Add(new SelectListItem { Text = "Select Menu", Value = "0", Selected = true });
                menu_id.AddRange(item_menu_id);
                ViewBag.Menu_Id = menu_id;

                List<SelectListItem> Menu_ID = new List<SelectListItem>();

                Menu_ID.Add(new SelectListItem { Text = "False", Value = "False", Selected = true });
                Menu_ID.Add(new SelectListItem { Text = "True", Value = "True" });

                ViewBag.isEnable = Menu_ID;

                return View();
            }
        }

        //
        // POST: /Categories/Create

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Categories categories)
        {
            if (ModelState.IsValid)
            {
                if (categories.ParentID == 0)
                {
                    categories.isParent = true;
                }
                db.Category.Add(categories);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categories);
        }

        //
        // GET: /Categories/Edit/5

        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id = 0)
        {
            Categories categories = db.Category.Find(id);
            if (categories == null)
            {
                return HttpNotFound();
            }

            List<SelectListItem> parent_id = new List<SelectListItem>();



            var item_parent_id = (from c in db.Category
                                  where c.isParent == true
                                  orderby c.Order
                                  select c).ToList().Select(u => new SelectListItem
                                  {
                                      Text = u.Name,
                                      Value = u.ID.ToString()
                                  });

            parent_id.Add(new SelectListItem { Text = "isParent", Value = "0", Selected = true });
            parent_id.AddRange(item_parent_id);
            ViewBag.Parent_Id = parent_id;

            List<SelectListItem> menu_id = new List<SelectListItem>();
            var item_menu_id = (from c in db.Menu
                                where c.isGroup == false
                                orderby c.Order
                                select c).ToList().Select(u => new SelectListItem
                                {
                                    Text = u.Name,
                                    Value = u.ID.ToString()
                                });

            menu_id.Add(new SelectListItem { Text = "Select Menu", Value = "0", Selected = true });
            menu_id.AddRange(item_menu_id);
            ViewBag.Menu_Id = menu_id;

            List<SelectListItem> Menu_ID = new List<SelectListItem>();

            Menu_ID.Add(new SelectListItem { Text = "False", Value = "False", Selected = true });
            Menu_ID.Add(new SelectListItem { Text = "True", Value = "True" });

            ViewBag.isEnable = Menu_ID;

            return View(categories);
        }

        //
        // POST: /Categories/Edit/5
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Categories categories)
        {
            if (ModelState.IsValid)
            {
                if(categories.ParentID == 0)
                {
                    categories.isParent = true;
                }
                db.Entry(categories).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categories);
        }

        //
        // GET: /Categories/Delete/5

        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id = 0)
        {
            Categories categories = db.Category.Find(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }

        //
        // POST: /Categories/Delete/5


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Categories categories = db.Category.Find(id);
            db.Category.Remove(categories);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }


        public ActionResult GetCategoryName(int id)
        {
            string CategoryTitle = "";

            try
            {
                CategoryTitle = (from m in db.Category
                                 where m.ID == id
                                 select (m.Title != "") ? m.Title : m.Name).FirstOrDefault().ToString();
            }
            catch
            {
                CategoryTitle = "Null";
            }

            return PartialView((object)CategoryTitle);
        }
    }
}