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
                           select new DisplayBook() { Author = BR.BookReadAuthor, Name = BR.BookReadName, Genre = BR.BookReadGenre, DateEntered = PB.BookDateEntered, BookReadID = BR.BookReadID, PersonBookID = PB.PersonBookID });
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
        public ActionResult Create([Bind(Include = "BookReadID, Name,Author,Genre, DateEntered")] DisplayBook displayBook)
        {
            if (ModelState.IsValid)
            {
                var addBook = (from a in db.BooksReads
                               where a.BookReadName == displayBook.Name
                               select a);
                var us = User.Identity.Name;
                var user = db.Users.FirstOrDefault(x => x.Email == us);

                if (addBook.Count() == 0)
                {
                    var newBook = new BooksRead();
                    newBook.BookReadName = displayBook.Name;
                    newBook.BookReadGenre = displayBook.Genre;
                    newBook.BookReadAuthor = displayBook.Author;

                    db.BooksReads.Add(newBook);


                    var newPB = new PersonBook();
                    newPB.BookDateEntered = displayBook.DateEntered;
                    newPB.BookReadID = newBook.BookReadID;
                    newPB.Id = user.Id;

                   
                    db.PersonBooks.Add(newPB);
                    db.SaveChanges();
                    
                    
                   
                }
                else
                {
                    var newPB = new PersonBook();
                    newPB.BookDateEntered = displayBook.DateEntered;
                    newPB.BookReadID = addBook.First().BookReadID;
                    newPB.Id = user.Id;

                    db.PersonBooks.Add(newPB);
                    db.SaveChanges();

                }
                return RedirectToAction("Index");
            }

            return View(displayBook);
        }

        // GET: BooksRead/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var us = User.Identity.Name;
            var user = db.Users.FirstOrDefault(x => x.Email == us);


            var books = db.PersonBooks.Where(x => x.Id == user.Id);
            var results = (from PB in db.PersonBooks
                           join BR in db.BooksReads on PB.BookReadID equals BR.BookReadID
                           where PB.Id == user.Id && PB.PersonBookID == id.Value
                           select new DisplayBook() { Author = BR.BookReadAuthor, Name = BR.BookReadName, Genre = BR.BookReadGenre, DateEntered = PB.BookDateEntered, BookReadID = BR.BookReadID, PersonBookID = PB.PersonBookID });


            if (results == null)
            {
                return HttpNotFound();
            }
            return View(results.First());
        }

        // POST: BooksRead/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookReadID, Name,Author,Genre, DateEntered")] DisplayBook displayBook)
        {
            
            if (ModelState.IsValid)
            {
                var us = User.Identity.Name;
                var user = db.Users.FirstOrDefault(x => x.Email == us);


                var books = db.PersonBooks.Where(x => x.Id == user.Id);
                var personBook = (from PB in db.PersonBooks
                                  where PB.PersonBookID == displayBook.PersonBookID
                                  select PB).First();
                personBook.BookDateEntered = displayBook.DateEntered;


                var bookReads = (from BR in db.BooksReads
                                 where BR.BookReadID == displayBook.BookReadID
                                 select BR).First();
                bookReads.BookReadName = displayBook.Name;
                bookReads.BookReadAuthor = displayBook.Author;
                bookReads.BookReadGenre = displayBook.Genre;
                db.Entry(bookReads).State = EntityState.Modified;

                db.Entry(personBook).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(displayBook);
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
