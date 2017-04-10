using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Datanomics.Models;

namespace Web.Datanomics.Controllers
{
    public class MenuController : Controller
    {
        private DataContext db = new DataContext();

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public ActionResult Index()
        {
            using(var db = new DataContext())
            {
                var model = from m in db.Menu
                            select m;
                return View(model.ToList());
            }
            
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public ActionResult Edit(int ID)
        {
            using (var db = new DataContext())
            {
                List<SelectListItem> item_parent_id = new List<SelectListItem>();
                var items = (from c in db.Menu
                             orderby c.Order
                             select c).ToList().Select(u => new SelectListItem
                             {
                                 Text = u.Name,
                                 Value = u.ID.ToString()
                             });
                item_parent_id.Add(new SelectListItem { Text = "isParent", Value = "0", Selected = true });
                item_parent_id.AddRange(items);
                ViewBag.List = item_parent_id;

                List<SelectListItem> item = new List<SelectListItem>();

                item.Add(new SelectListItem { Text = "False", Value = "False", Selected = true });
                item.Add(new SelectListItem { Text = "True", Value = "True" });

                ViewBag.isEnable = item;

                Menu menu = db.Menu.Find(ID);
                if (menu == null)
                {
                    return HttpNotFound();
                }
                return View(menu);
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult Edit(Menu model)
        {
            if (model.ParentID == 0)
            {
                model.isParent = true;
            }
            else
            {
                model.isParent = false;
            }
            if (model.isGroup)
            {
                model.isDropdown = true;
            }
            else
            {
                model.isDropdown = model.isDropdown;
            }

            if (ModelState.IsValid)
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);

        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public ActionResult Create()
        {
            using(var db = new DataContext())
            {
                List<SelectListItem> item_parent_id = new List<SelectListItem>();



                var items = (from c in db.Menu
                             where c.isParent == true
                             orderby c.Order
                             select c).ToList().Select(u => new SelectListItem
                             {
                                 Text = u.Name,
                                 Value = u.ID.ToString()
                             });

                item_parent_id.Add(new SelectListItem { Text = "isParent", Value = "0", Selected = true });
                item_parent_id.AddRange(items);

                ViewBag.List = item_parent_id;

                List<SelectListItem> item = new List<SelectListItem>();

                item.Add(new SelectListItem { Text = "False", Value = "False", Selected = true });
                item.Add(new SelectListItem { Text = "True", Value = "True" });

                ViewBag.isEnable = item;

                return View();
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Menu model)
        {
            if(model.ParentID == 0)
            {
                model.isParent = true;
            }
            else
            {
                model.isParent = false;
            }
            if (model.isGroup)
            {
                model.isDropdown = true;
            }
            else
            {
                model.isDropdown = model.isDropdown;
            }
            if (ModelState.IsValid)
            {
                db.Menu.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
            
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int ID = 0)
        {
            Menu menu = db.Menu.Find(ID);
            if (menu == null)
            {
                return HttpNotFound();
            }
            else
            {
                db.Menu.Remove(menu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }


        // GET: /Menu/
  
        public PartialViewResult Menu()
        {
            using (var db = new DataContext())
            {
                var menu = from m in db.Menu
                           where (m.isParent == true && m.Visible == true)
                           orderby m.Order ascending
                           select m;
                return PartialView(menu.ToList());
            }
        }
        [ChildActionOnly]
        public PartialViewResult ChildrenMenu(int ParentId)
        {
            using (var db = new DataContext())
            {
                var _menu = from c in db.Menu
                            where c.ParentID == ParentId
                            orderby c.Order ascending
                            select c;
                return PartialView(_menu.ToList());
            }
        }

        public PartialViewResult Categories(int MenuId)
        {
            using (var db = new DataContext())
            {
                var menu = from c in db.Category
                           join m in db.Menu
                           on c.MenuID equals m.ID
                           where (c.MenuID == MenuId && c.Visible == true)
                           orderby c.Order ascending
                           select new CategoriesView
                           {
                               ID = c.ID,
                               Name = c.Name,
                               Title = c.Title,
                               Target = c.Target,
                               ParentID = c.ParentID,
                               isParent = c.isParent,
                               IconClass = c.IconClass,
                               MenuIsParent = m.isParent
                           };

                return PartialView(menu.ToList());
            }
        }

        public PartialViewResult Category()
        {
            var menu = from c in db.Category
                       where c.Visible == true
                       orderby c.Order ascending
                       select c;
            return PartialView(menu.ToList());
        }

        [ChildActionOnly]
        public PartialViewResult ChildrenCategories(int ParentId)
        {
            using (var db = new DataContext())
            {
                var _menu = from c in db.Category
                            where (c.ParentID == ParentId && c.Visible == true)
                            select c;

                return PartialView(_menu.ToList());
            }

        }

        public ActionResult GetParentName(int ParentID)
        {
            string ParentName = "";

            try
            {
                ParentName = (from m in db.Menu
                              where m.ID == ParentID
                              select (m.Title!= "")? m.Title: m.Name).FirstOrDefault().ToString();
            }
            catch
            {
                ParentName = "Null";
            }

            return PartialView((object)ParentName);
        }
    }
}
