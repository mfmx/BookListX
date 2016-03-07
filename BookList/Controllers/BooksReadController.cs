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
    [Authorize]
    public class BooksReadController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BooksRead
        public ActionResult Index()
        {

            var us = User.Identity.Name;
            var user = db.Users.FirstOrDefault(x => x.Email == us);

            
            var books = db.PersonBooks.Where(x => x.Id == user.Id);
            var results = (from PB in db.PersonBooks
                           join BR in db.BooksReads on PB.BookReadID equals BR.BookReadID
                           where PB.Id == user.Id
                           select new DisplayBook() { Author = BR.BookReadAuthor, Name = BR.BookReadName, Genre = BR.BookReadGenre, DateEntered = PB.BookDateEntered, BookReadID = BR.BookReadID });
            return View(results);

        }

        [Authorize(Roles = "Admin")]
        public ActionResult List()
        {
            return View(db.BooksReads.ToList());
        }


        // GET: BooksRead/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BooksRead booksRead = db.BooksReads.Find(id);
            if (booksRead == null)
            {
                return HttpNotFound();
            }
            return View(booksRead);
        }

        [Authorize(Roles ="Admin")]
        public ActionResult DetailsForAdmin(int id)
        {
            var bks = db.BooksReads.Find(id);
            return View("Details", bks);
        }

        // GET: BooksRead/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BooksRead/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookReadID, Name,Author,Genre, DateEntered")] BooksRead booksRead)
        {
            if (ModelState.IsValid)
            {
                db.BooksReads.Add(booksRead);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(booksRead);
        }

        // GET: BooksRead/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BooksRead booksRead = db.BooksReads.Find(id);
            if (booksRead == null)
            {
                return HttpNotFound();
            }
            return View(booksRead);
        }

        // POST: BooksRead/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookReadID,BookReadName,BookReadAuthor,BookReadGenre, BookEnteredDate")] BooksRead booksRead)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booksRead).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(booksRead);
        }

        // GET: BooksRead/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BooksRead booksRead = db.BooksReads.Find(id);
            if (booksRead == null)
            {
                return HttpNotFound();
            }
            return View(booksRead);
        }

        // POST: BooksRead/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BooksRead booksRead = db.BooksReads.Find(id);
            db.BooksReads.Remove(booksRead);
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
