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
                           select new DisplayWishList() { Author = BR.BookReadAuthor, Name = BR.BookReadName, Genre = BR.BookReadGenre, DateEntered = WL.BookDateEntered, BookReadID = BR.BookReadID, WishListID = WL.WishListID });
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
        public ActionResult Create([Bind(Include = "WishListID,Name,Author,Genre, DateEntered")] DisplayWishList displayWishList)
        {
            var us = User.Identity.Name;
            var user = db.Users.FirstOrDefault(x => x.Email == us);

            if (ModelState.IsValid)
            {
                var addBook = (from a in db.BooksReads
                               where a.BookReadName == displayWishList.Name
                               select a);


                if (addBook.Count() == 0)
                {
                    var newBook = new BooksRead();
                    newBook.BookReadName = displayWishList.Name;
                    newBook.BookReadGenre = displayWishList.Genre;
                    newBook.BookReadAuthor = displayWishList.Author;

                    db.BooksReads.Add(newBook);
                    db.SaveChanges();


                    var newWL = new WishList();
                    newWL.BookDateEntered = displayWishList.DateEntered;
                    newWL.BookReadID = newBook.BookReadID;
                    newWL.Id = user.Id;


                    db.WishLists.Add(newWL);
                    db.SaveChanges();



                }
                else
                {
                    var newWL = new WishList();
                    newWL.BookDateEntered = displayWishList.DateEntered;
                    newWL.BookReadID = addBook.First().BookReadID;
                    newWL.Id = user.Id;

                    db.WishLists.Add(newWL);
                    db.SaveChanges();

                }
                return RedirectToAction("Index");
            }

            return View(displayWishList);
        }

        // GET: WishList/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var us = User.Identity.Name;
            var user = db.Users.FirstOrDefault(x => x.Email == us);


            var books = db.WishLists.Where(x => x.Id == user.Id);
            var results = (from WL in db.WishLists
                           join BR in db.BooksReads on WL.BookReadID equals BR.BookReadID
                           where WL.Id == user.Id && WL.WishListID == id.Value
                           select new DisplayWishList() { Author = BR.BookReadAuthor, Name = BR.BookReadName, Genre = BR.BookReadGenre, DateEntered = WL.BookDateEntered, BookReadID = BR.BookReadID, WishListID = WL.WishListID });


            if (results == null)
            {
                return HttpNotFound();
            }
            return View(results.First());
        }

        // POST: WishList/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WishListID,Name,Author,Genre, DateEntered, BookReadID")] DisplayWishList displayWishList)
        {
            if (ModelState.IsValid)
            {
                var us = User.Identity.Name;
                var user = db.Users.FirstOrDefault(x => x.Email == us);


                var books = db.WishLists.Where(x => x.Id == user.Id);
                var wishList = (from WL in db.WishLists
                                  where WL.WishListID == displayWishList.WishListID
                                  select WL).First();
                wishList.BookDateEntered = displayWishList.DateEntered;


                var bookReads = (from BR in db.BooksReads
                                 where BR.BookReadID == displayWishList.BookReadID
                                 select BR).First();
                bookReads.BookReadName = displayWishList.Name;
                bookReads.BookReadAuthor = displayWishList.Author;
                bookReads.BookReadGenre = displayWishList.Genre;
                db.Entry(bookReads).State = EntityState.Modified;

                db.Entry(wishList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(displayWishList);
        }

        // GET: WishList/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var us = User.Identity.Name;
            var user = db.Users.FirstOrDefault(x => x.Email == us);


            var books = db.WishLists.Where(x => x.Id == user.Id);
            var results = (from WL in db.WishLists
                           join BR in db.BooksReads on WL.BookReadID equals BR.BookReadID
                           where WL.Id == user.Id && WL.WishListID == id.Value
                           select new DisplayWishList() { Author = BR.BookReadAuthor, Name = BR.BookReadName, Genre = BR.BookReadGenre, DateEntered = WL.BookDateEntered, BookReadID = BR.BookReadID, WishListID = WL.WishListID });
            if (results.Count() == 0)
            {
                return HttpNotFound();
            }
            return View(results.First());
        }

        // POST: WishList/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WishList bookWish = db.WishLists.Find(id);
            db.WishLists.Remove(bookWish);
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
