//-----------------------------------------------------------------------
// <copyright file="BooksController.cs" company="Laurent Perruche-Joubert">
//     © 2013 Laurent Perruche-Joubert
// </copyright>
//-----------------------------------------------------------------------
namespace Pop.Web.Controllers {
    using System;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Pop.Domain;
    using Pop.Domain.Entities;

    using Pop.Web.ViewModels;

    /// <summary>
    /// Books controller
    /// </summary>
    public class BooksController : Controller {
        /// <summary>
        /// Index action
        /// </summary>
        /// <returns>An ActionResult</returns>
        [AllowAnonymous]
        public ActionResult Index() {
            using (var uow = new UnitOfWork(false)) {
                var books = uow.Books.All().OrderBy(x => x.Title).ToList();
                return View(books);
            }
        }

        /// <summary>
        /// View a book action
        /// </summary>
        /// <param name="id">A book id</param>
        /// <returns>An ActionResult</returns>
        [AllowAnonymous]
        public ActionResult View(int id) {
            using (var uow = new UnitOfWork(false)) {
                var book = uow.Books.Find(id);

                if (book == null) {
                    throw new HttpException(404, "Impossible de trouver le livre correspondant à l'identifiant n°" + id);
                } else {
                    book.InitializeChartInfos();
                    return View(book);
                }
            }
        }

        /// <summary>
        /// New book action
        /// </summary>
        /// <returns>An ActionResult</returns>
        public ActionResult New() {
            return View(new Book());
        }

        /// <summary>
        /// Edit a book action
        /// </summary>
        /// <param name="id">A book id</param>
        /// <returns>An ActionResult</returns>
        public ActionResult Edit(int id) {
            using (var uow = new UnitOfWork(false)) {
                var book = uow.Books.Find(id);
                return View(book);
            }
        }

        /// <summary>
        /// Creates or updates a book and redirect to the View action
        /// </summary>
        /// <param name="book">A new book to create</param>
        /// <param name="coverFile">An uploaded file (can be null)</param>
        /// <returns>An ActionResult</returns>
        [HttpPost]
        public ActionResult SaveOrUpdate(Book book, HttpPostedFileBase coverFile) {
            if (!string.IsNullOrEmpty(book.CoverFileName)) {
                book.CoverFileName = Path.GetFileName(book.CoverFileName);
            }

            if (coverFile != null && coverFile.ContentLength > 0) {
                var fileName = Path.GetFileName(coverFile.FileName);
                var directory = Server.MapPath("~/Content/images/books");
                if (!Directory.Exists(directory)) {
                    Directory.CreateDirectory(directory);
                }

                coverFile.SaveAs(Path.Combine(directory, fileName));
            }

            using (var uow = new UnitOfWork(true)) {
                if (book.Id != 0) {
                    var oldBook = uow.Books.Find(book.Id);
                    oldBook.Title = book.Title;
                    oldBook.Genre = book.Genre;
                    oldBook.Writer = book.Writer;
                    oldBook.BookSeries = book.BookSeries;
                    oldBook.BookNumber = book.BookNumber;
                    oldBook.PublicationDate = book.PublicationDate;
                    oldBook.CoverFileName = book.CoverFileName;
                    oldBook.Summary = book.Summary;
                    oldBook.WikipediaLink = book.WikipediaLink;
                    uow.Books.SaveOrUpdate(oldBook);
                } else {
                    uow.Books.SaveOrUpdate(book);
                }
                
                uow.Commit();
            }

            return RedirectToAction("View", new { id = book.Id });
        }

        /// <summary>
        /// Returns a JSON list of books with title matching the given start
        /// </summary>
        /// <param name="start">A title start</param>
        /// <returns>A JSON list of books</returns>
        public JsonResult AllMatchingTitles(string start) {
            using (var uow = new UnitOfWork(false)) {
                var books = uow.Books.AllByTitleStart(start);
                return Json(books);
            }
        }

        /// <summary>
        /// Saves a new reading session and creates a new book if necessary
        /// </summary>
        /// <param name="bookEntry">A book entry</param>
        /// <returns>A JSON representation of a timeline entry</returns>
        public JsonResult QuickSave(BookEntry bookEntry) {
            // Used to emulate a failure
            if (bookEntry.Writer == "FAIL") {
                throw new Exception("Une erreur");
            }

            Book book;
            ReadingSession readingSession;
            using (var uow = new UnitOfWork(true)) {
                book = uow.Books.FindByTitle(bookEntry.Title);
                if (book == null) {
                    book = new Book() { Title = bookEntry.Title, Writer = bookEntry.Writer };
                }

                readingSession = new ReadingSession() { 
                    Date = DateTime.ParseExact(bookEntry.EntryDate, "dd/MM/yyyy", null),
                    Note = bookEntry.Note };
                book.AddReadingSession(readingSession);

                uow.Books.SaveOrUpdate(book);
                uow.Commit();
            }

            var timelineEntry = new TimelineEntry(readingSession, this);
            return Json(timelineEntry);
        }
    }
}
