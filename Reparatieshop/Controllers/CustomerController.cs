﻿using Microsoft.AspNet.Identity;
using PagedList;
using Reparatieshop.DAL;
using Reparatieshop.Models;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Reparatieshop.Controllers
{
    [Authorize(Roles = "Customer, Administrator")]
    public class CustomerController : BaseController
    {
        public CustomerController() : base()
        {
        }

        public CustomerController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, ApplicationRoleManager roleManager)
            : base(userManager, signInManager, roleManager)
        {
        }

        private ShopContext db = new ShopContext();

        // GET: Customer
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            if (User.IsInRole("Administrator"))
            {
                ViewBag.CurrentSort = sortOrder;
                ViewBag.LastNameSortParm = string.IsNullOrEmpty(sortOrder) ? "lastname_desc" : "";
                ViewBag.FirstNameSortParm = sortOrder == "firstname_asc" ? "firstname_desc" : "firstname_asc";

                if (searchString != null)
                {
                    page = 1;
                }
                else
                {
                    searchString = currentFilter;
                }

                ViewBag.CurrentFilter = searchString;

                #region example code
                //IQueryable<Customer> customers = from c in db.Customers select c;
                #endregion
                IQueryable<Customer> customers = db.Customers;
                if (!string.IsNullOrEmpty(searchString))
                {
                    customers = customers.Where(c => c.LastName.Contains(searchString)
                                           || c.FirstName.Contains(searchString));
                }
                switch (sortOrder)
                {
                    case "lastname_desc":
                        customers = customers.OrderByDescending(c => c.LastName);
                        break;

                    case "firstname_asc":
                        customers = customers.OrderBy(c => c.FirstName);
                        break;

                    case "firstname_desc":
                        customers = customers.OrderByDescending(c => c.FirstName);
                        break;

                    default:
                        customers = customers.OrderBy(c => c.LastName);
                        break;
                }
                // did not implement using PagedList;
                //int pageSize = 3;
                int pageNumber = (page ?? 1);
                //return View(customers.ToPagedList(pageNumber, pageSize));
                return View(customers.ToPagedList(pageNumber, customers.Count()));
            }
            else
            {
                string custId = User.Identity.GetUserId();
                IQueryable<Customer> customers = db.Customers.Where(c => c.CustomerId == custId);
                return View(customers);
            }
        }

        // GET: Customer/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customer/Create
        [AllowAnonymous]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Email,Password,ConfirmPassword,FirstName,LastName,DoB,City,Street,Zipcode,HouseNumber")] RegisterCustomerViewModel customer)
        {
            customer.SelectedRoleID = "Customer";
            try
            {
                if (ModelState.IsValid)
                {
                    ApplicationUser user = new ApplicationUser { UserName = customer.Email, Email = customer.Email };
                    IdentityResult result = await UserManager.CreateAsync(user, customer.Password);

                    if (result.Succeeded)
                    {
                        result = await UserManager.AddToRoleAsync(user.Id, customer.SelectedRoleID);
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        await UserManager.AddToRoleAsync(user.Id, "Customer");

                        customer.CustomerId = user.Id;
                        db.Customers.Add(new Customer(customer));
                        db.SaveChanges();

                        return RedirectToAction("Index", "Home");
                    }
                    return RedirectToAction("Index");
                }
            }
            catch (DataException Dex)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                return View("Error", new HandleErrorInfo(Dex, "Customer", "Create"));
            }
            return View(customer);
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customer/Edit/5
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
            Customer customerToUpdate = db.Customers.Find(id);
            if (TryUpdateModel(customerToUpdate, "", new string[] { "CustomerId", "FirstName", "LastName", "DoB", "City", "Street", "Zipcode", "HouseNumber" }))
            {
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (DataException Dex)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                    return View("Error", new HandleErrorInfo(Dex, "Customer", "EditPost"));
                }
            }
            return View(customerToUpdate);
        }

        // GET: Customer/Delete/5
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
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customer/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Customer customer = db.Customers.Find(id);
                db.Customers.Remove(customer);
                db.SaveChanges();
            }
            catch (DataException /*Dex*/)
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