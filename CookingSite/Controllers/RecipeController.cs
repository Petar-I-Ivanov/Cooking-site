using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CookingSite.DataAccessLayer;
using CookingSite.Models;
using PagedList;

namespace CookingSite.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class RecipeController : Controller
    {
        private CookingSiteContext db = new CookingSiteContext();

        // GET: Recipe
        public ActionResult Index(string sortOrder, string currentFilter, string search, int? page)
        {

            ViewBag.FunctionShow = User.Identity.IsAuthenticated ? "Yes" : "No";
            ViewBag.AdminFuncShow = (User.Identity.AuthenticationType.Equals("Admin")
                || User.Identity.Name.Equals("admin@abv.bg") ) ? "Yes" : "No";

            ViewBag.CurrentSort = sortOrder;
            sortOrder = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.TitleSort = sortOrder;

            if (search != null)
            {
                page = 1;
            }
            else
            {
                search = currentFilter;
            }

            ViewBag.CurrentFilter = search;

            var recipes = from r in db.Recipes
                          select r;

            if (!String.IsNullOrEmpty(search))
                recipes = recipes.Where(r => r.Title.Contains(search) || r.Category.Title.Contains(search));

            recipes = (sortOrder.Equals("title_desc"))
                ? recipes.OrderByDescending(r => r.Title)
                : recipes.OrderBy(r => r.Title);

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(recipes.ToPagedList(pageNumber, pageSize));
        }

        // GET: Recipe/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = db.Recipes.Find(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
        }

        // GET: Recipe/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Title");
            var dateAndTime = DateTime.Now;
            var date = dateAndTime.Date;
            return View(new Recipe { PublishedDate = date });
        }

        // POST: Recipe/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CategoryId,Title,Desription,NeededProducts,PublishedDate")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                db.Recipes.Add(recipe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Title", recipe.CategoryId);
            return View(recipe);
        }

        // GET: Recipe/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = db.Recipes.Find(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Title", recipe.CategoryId);
            return View(recipe);
        }

        // POST: Recipe/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CategoryId,Title,Desription,NeededProducts,PublishedDate")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recipe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Title", recipe.CategoryId);
            return View(recipe);
        }

        // GET: Recipe/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = db.Recipes.Find(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
        }

        // POST: Recipe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Recipe recipe = db.Recipes.Find(id);
            db.Recipes.Remove(recipe);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
