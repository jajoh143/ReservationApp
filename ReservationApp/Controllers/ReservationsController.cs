using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ReservationApp.DAL;
using ReservationApp.Models;

namespace ReservationApp.Controllers
{
    public class ReservationsController : Controller
    {
        private ReservationContext db = new ReservationContext();

        // GET: Reservations
        public ViewResult Index(string sortOrder, string searchString)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.DSortParm = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "Date";
            ViewBag.EmployeeName = sortOrder == "EmployeeName" ? "EmpName_desc" : "";
            ViewBag.ClientName = sortOrder == "ClientName" ? "CName_desc" : "ClientName";
            

            var reservations = from x in db.Reservations
                            select x;

            if (!String.IsNullOrEmpty(searchString))
            {
                reservations = reservations.Where(x => x.Client.LastName.Contains(searchString)
                     || x.Employee.LastName.Contains(searchString) 
                     || x.Client.FirstName.Contains(searchString)
                     || x.Employee.FirstName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "CName_desc":
                    reservations = reservations.OrderByDescending(x => x.Client.LastName);
                    break;
                case "ClientName":
                    reservations = reservations.OrderBy(x => x.Client.LastName);
                    break;
                case "EmpName_desc":
                    reservations = reservations.OrderByDescending(x => x.Employee.LastName);
                    break;
                case "EmployeeName":
                    reservations = reservations.OrderBy(x => x.Employee.LastName);
                    break;
                case "Date":
                    reservations = reservations.OrderBy(x=>x.Date);
                    break;
                case "date_desc":
                    reservations = reservations.OrderByDescending(x=>x.Date);
                    break;
                default:
                    reservations = reservations.OrderBy(x => x.Date);
                    break;

            }
            return View(reservations.ToList());
        }

        // GET: Reservations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // GET: Reservations/Create
        public ActionResult Create()
        {
            ViewBag.ClientID = new SelectList(db.Clients, "ID", "LastName");
            ViewBag.EmployeeID = new SelectList(db.Employees, "ID", "LastName");
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReservationID,ClientID,EmployeeID,Date,Time,ClientName,EmployeeName")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Reservations.Add(reservation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientID = new SelectList(db.Clients, "ID", "LastName", reservation.ClientID);
            ViewBag.EmployeeID = new SelectList(db.Employees, "ID", "LastName", reservation.EmployeeID);
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientID = new SelectList(db.Clients, "ID", "LastName", reservation.ClientID);
            ViewBag.EmployeeID = new SelectList(db.Employees, "ID", "LastName", reservation.EmployeeID);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReservationID,ClientID,EmployeeID,Date,Time,ClientName,EmployeeName")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientID = new SelectList(db.Clients, "ID", "LastName", reservation.ClientID);
            ViewBag.EmployeeID = new SelectList(db.Employees, "ID", "LastName", reservation.EmployeeID);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            db.Reservations.Remove(reservation);
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
