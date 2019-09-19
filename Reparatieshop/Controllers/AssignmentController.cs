﻿using Reparatieshop.DAL;
using Reparatieshop.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Reparatieshop.Controllers
{
    public class AssignmentController : Controller
    {
        private ShopContext db = new ShopContext();

        // GET: Assignment
        public ActionResult Index(string sortOrder)
        {
            ViewBag.Status = Enum.GetNames(typeof(Status));

            IQueryable<Assignment> assignments;

            assignments = db.Assignments;
            var b = assignments.ToList();

            ViewBag.Pending = $"Pending: {assignments.Where(x => x.Status == Status.Pending).Count()}";
            ViewBag.in_progress = $"in progress: {assignments.Where(x => x.Status == Status.in_progress).Count()}";
            ViewBag.Waiting_for_parts = $"Waiting for parts: {assignments.Where(x => x.Status == Status.Waiting_for_parts).Count()}";
            ViewBag.Done = $"Done: {assignments.Where(x => x.Status == Status.Done).Count()}";


            return View(b);
        }

        // GET: Assignment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignment assignment = db.Assignments.Find(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }
            return View(assignment);
        }

        // GET: Assignment/Create
        public ActionResult Create()
        {
            List<string> customers = new List<string>();
            foreach (var customer in db.Customers)
            {
                customers.Add(customer.FirstName + " " + customer.LastName);
            }

            //List<int> customers = new List<int>();
            //foreach (var item in db.Customers)
            //{
            //    customers.Add(item.CustomerId);
            //}

            ViewBag.Customers = customers;

            return View();
        }

        // POST: Assignment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AssignmentId,Start,End,Status,Description,HoursWorked")] Assignment assignment)
        {
            if (ModelState.IsValid)
            {
                Customer customer = IdentifyByName(Request.Form["CustomerId"]);
                assignment.Customer = customer;
                db.Assignments.Add(assignment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(assignment);
        }

        private Customer IdentifyByName(string name)
        {
            string firstName = name.Substring(0,name.IndexOf(' '));
            string lastName = name.Substring(name.IndexOf(' ')+1, name.Length - (firstName.Length +1));
            Customer customerSelected = new Customer();
            foreach (var customer in db.Customers)
            {
                if (firstName == customer.FirstName && lastName == customer.LastName)
                {
                    customerSelected = customer;
                    break;
                }
            }
            return customerSelected;
        }

        // GET: Assignment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignment assignment = db.Assignments.Find(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }
            return View(assignment);
        }

        // POST: Assignment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignment assignmentToUpdate = db.Assignments.Find(id);
            if (TryUpdateModel(assignmentToUpdate, "", new string[] { "AssignmentId", "Start", "End", "Status", "Description", "HoursWorked" }))
            {
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (DataException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(assignmentToUpdate);
        }

        // GET: Assignment/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            Assignment assignment = db.Assignments.Find(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }
            return View(assignment);
        }

        // POST: Assignment/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Assignment assignment = db.Assignments.Find(id);
                db.Assignments.Remove(assignment);
                db.SaveChanges();
            }
            catch (DataException/* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
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