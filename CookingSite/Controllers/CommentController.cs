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
    public class CommentController : Controller
    {
        private CookingSiteContext db = new CookingSiteContext();

        // GET: Comment
        public ActionResult Index(string sortOrder, string currentFilter, string search, int? page)
        {

            ViewBag.FunctionShow = User.Identity.IsAuthenticated ? "Yes" : "No";
            ViewBag.AdminFuncShow = (User.Identity.AuthenticationType.Equals("Admin")
                || User.Identity.Name.Equals("admin@abv.bg")) ? "Yes" : "No";

            ViewBag.CurrentSort = sortOrder;
            sortOrder = String.IsNullOrEmpty(sortOrder) ? "author_desc" : "";
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

            var comments = from c in db.Comments
                          select c;

            if (!String.IsNullOrEmpty(search))
                comments = comments.Where(r => r.Content.Contains(search) || r.Recipe.Title.Contains(search));

            comments = (sortOrder.Equals("author_desc"))
                ? comments.OrderByDescending(r => r.Author)
                : comments.OrderBy(r => r.Author);

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(comments.ToPagedList(pageNumber, pageSize));
        }

        // GET: Comment/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: Comment/Create
        public ActionResult Create()
        {
            ViewBag.RecipeId = new SelectList(db.Recipes, "Id", "Title");
            ViewBag.userName = User.Identity.Name;
            return View();
        }

        // POST: Comment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RecipeId,Content,Author,PublishedAt")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.PublishedAt = DateTime.Today;
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RecipeId = new SelectList(db.Recipes, "Id", "Title", comment.RecipeId);
            return View(comment);
        }

        // GET: Comment/UserCreate/5
        public ActionResult UserCreate(long? id)
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
            Comment comment = new Comment();
            comment.RecipeId = (long) id;
            comment.Recipe = recipe;
            comment.Author = User.Identity.Name;
            comment.PublishedAt = DateTime.Today;
            return View(comment);
        }

        // GET: Comment/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.RecipeId = new SelectList(db.Recipes, "Id", "Title", comment.RecipeId);
            return View(comment);
        }

        // POST: Comment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RecipeId,Content,Author,PublishedAt")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.PublishedAt = DateTime.Today;
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RecipeId = new SelectList(db.Recipes, "Id", "Title", comment.RecipeId);
            return View(comment);
        }

        public ActionResult UserEdit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null || comment.Author != User.Identity.Name)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: Comment/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
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
