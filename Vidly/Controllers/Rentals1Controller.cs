using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using AutoMapper;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class Rentals1Controller : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Rentals1
        public ActionResult Index()
        {
            return View(db.Rentals
                .Include(x => x.Customer)
                .Include(x => x.Book).ToList());
        }


        // GET: Rentals1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rental rental = db.Rentals
                .Include(x => x.Customer)
                .Include(x => x.Book)
                .FirstOrDefault(x=>x.Id==id);
            if (rental == null)
            {
                return HttpNotFound();
            }
            return View(rental);
        }

        // GET: Rentals1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rentals1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DateRented,DateReturned")] Rental rental)
        {
            if (ModelState.IsValid)
            {
                db.Rentals.Add(rental);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rental);
        }

        // GET: Rentals1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rental rental = db.Rentals.Find(id);
            if (rental == null)
            {
                return HttpNotFound();
            }

            var dto = new EditRentalDto()
            {
                Id = rental.Id,
                DateRented = rental.DateRented,
                DateReturned = rental.DateReturned
            };
            return View(dto);
        }

        // POST: Rentals1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DateRented,DateReturned")] EditRentalDto rentaldto)
        {
            if (ModelState.IsValid)
            {
                var rental = db.Rentals.Include(x => x.Customer)
                    .Include(x => x.Book).FirstOrDefault(x=>x.Id==rentaldto.Id);
                if (rentaldto != null)
                {
                    rental.DateRented = rentaldto.DateRented;
                    rental.DateReturned = rentaldto.DateReturned;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(rentaldto);
        }

        // GET: Rentals1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rental rental = db.Rentals.Find(id);
            if (rental == null)
            {
                return HttpNotFound();
            }
            return View(rental);
        }

        // POST: Rentals1/Delete/5
        [System.Web.Mvc.HttpPost, System.Web.Mvc.ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rental rental = db.Rentals.Find(id);
            db.Rentals.Remove(rental);
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
