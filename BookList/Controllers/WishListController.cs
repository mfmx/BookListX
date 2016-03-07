using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookList.Models;
using System.Web.Security;



namespace BookList.Controllers
{
    public class WishListController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: WishList
        public ActionResult Index()
        {
            var us = User.Identity.Name;
            var user = db.Users.FirstOrDefault(x => x.Email == us);


            var books = db.WishLists.Where(x => x.Id == user.Id);
            var results = (from WL in db.WishLists
                           join BR in db.BooksReads on WL.BookReadID equals BR.BookReadID
                           where WL.Id == user.Id
                           select new DisplayWishList() { Author = BR.BookReadAuthor, Name = BR.BookReadName, Genre = BR.BookReadGenre, DateEntered = WL.BookDateEntered, WishListID = BR.BookReadID });
            return View(results);
        }

        // GET: WishList/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BooksRead wishList = db.BooksReads.Find(id);
            if (wishList == null)
            {
                return HttpNotFound();
            }
            return View(wishList);
        }

        // GET: WishList/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WishList/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WishListID,Name,Author,Genre, DateEntered")] BooksRead wishList)
        {
            if (ModelState.IsValid)
            {
                db.BooksReads.Add(wishList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(wishList);
        }

        // GET: WishList/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BooksRead wishList = db.BooksReads.Find(id);
            if (wishList == null)
            {
                return HttpNotFound();
            }
            return View(wishList);
        }

        // POST: WishList/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WishListID,Name,Author,Genre, DateEntered")] BooksRead wishList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wishList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(wishList);
        }

        // GET: WishList/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BooksRead wishList = db.BooksReads.Find(id);
            if (wishList == null)
            {
                return HttpNotFound();
            }
            return View(wishList);
        }

        // POST: WishList/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BooksRead wishList = db.BooksReads.Find(id);
            db.BooksReads.Remove(wishList);
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
