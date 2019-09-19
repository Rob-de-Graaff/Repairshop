using Reparatieshop.DAL;
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
            List<Customer> customers =  db.Customers.ToList();
            ViewBag.Customers = customers;
            List<Repairer> repairers = db.Repairers.ToList();
            ViewBag.Repairers = repairers;

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
                Customer customer = db.Customers.Find(Request.Form["CustomerId"]);
                assignment.Customer = customer;
                Repairer repairer = db.Repairers.Find(Request.Form["RepairerId"]);
                assignment.Repairer = repairer;
                db.Assignments.Add(assignment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(assignment);
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

        public ActionResult AddProduct(string returnUrl, int? AssignmentId)
        {
            IEnumerable<Product> getProducts = db.Products.ToList();
            ViewBag.returnurl = returnUrl;
            ViewBag.AssignmentId = AssignmentId;

            return View(getProducts);

        }

        public ActionResult AddProducts(int? id, string returnUrl, int? AssignmentId)
        {
            if (id == null || AssignmentId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product tempProduct = db.Products.ToList().Where(s => s.ProductId == id).FirstOrDefault();

            if (tempProduct == null)
            {
                return HttpNotFound();
            }

            Assignment assignment = db.Assignments.Where(s => s.AssignmentId == AssignmentId).FirstOrDefault();

            assignment.Products.Add(tempProduct);

            db.Entry(assignment).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return Redirect(returnUrl);
        }
    }
}