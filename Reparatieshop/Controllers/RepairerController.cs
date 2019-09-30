using Reparatieshop.DAL;
using Reparatieshop.Extensions;
using Reparatieshop.Models;
using PagedList;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Reparatieshop.Controllers
{
    [Authorize(Roles ="Repairer, Administrator")]
    public class RepairerController : BaseController
    {
        private ShopContext db = new ShopContext();

        // GET: Repairer
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
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
            //var repairers = from r in db.Repairers select r;
            #endregion
            IQueryable<Repairer> repairers = db.Repairers;
            
            if (!string.IsNullOrEmpty(searchString))
            {
                repairers = repairers.Where(r => r.LastName.Contains(searchString)
                                       || r.FirstName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "lastname_desc":
                    repairers = repairers.OrderByDescending(r => r.LastName);
                    break;

                case "firstname_asc":
                    repairers = repairers.OrderBy(r => r.FirstName);
                    break;

                case "firstname_desc":
                    repairers = repairers.OrderByDescending(r => r.FirstName);
                    break;

                default:
                    repairers = repairers.OrderBy(r => r.LastName);
                    break;
            }
            // did not implement using PagedList;
            //int pageSize = 3;
            int pageNumber = (page ?? 1);
            //return View(repairers.ToPagedList(pageNumber, pageSize));
            return View(repairers.ToPagedList(pageNumber, repairers.Count()));
        }

        // GET: Repairer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Repairer repairer = db.Repairers.Find(id);
            if (repairer == null)
            {
                return HttpNotFound();
            }
            return View(repairer);
        }

        // GET: Repairer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Repairer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Email,Password,ConfirmPassword,FirstName,LastName,DoB,Wage")] RegisterRepairerViewModel repairer)
        {
            repairer.SelectedRoleID = "Repairer";
            try
            {
                if (ModelState.IsValid)
                {
                    ApplicationUser user = new ApplicationUser { UserName = repairer.Email, Email = repairer.Email };
                    IdentityResult result = await UserManager.CreateAsync(user, repairer.Password);

                    if (result.Succeeded)
                    {
                        result = await UserManager.AddToRoleAsync(user.Id, repairer.SelectedRoleID);
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        await UserManager.AddToRoleAsync(user.Id, "Repairer");

                        repairer.RepairerId = user.Id;
                        db.Repairers.Add(new Repairer(repairer));
                        db.SaveChanges();

                        return RedirectToAction("Index", "Home");
                    }
                    return RedirectToAction("Index");
                }
            }
            catch (DataException Dex)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                return View("Error", new HandleErrorInfo(Dex, "Repairer", "Create"));
            }

            return View(repairer);
        }

        // GET: Repairer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Repairer repairer = db.Repairers.Find(id);
            if (repairer == null)
            {
                return HttpNotFound();
            }
            return View(repairer);
        }

        // POST: Repairer/Edit/5
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
            Repairer repairerToUpdate = db.Repairers.Find(id);

            repairerToUpdate.Wage = DoubleExtension.ConvertInput(Request.Form["WageTextbox"]);

            if (TryUpdateModel(repairerToUpdate, "", new string[] { "RepairerId", "FirstName", "LastName", "DoB", "Wage" }))
            {
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (DataException Dex)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                    return View("Error", new HandleErrorInfo(Dex, "Repairer", "EditPost"));
                }
            }
            return View(repairerToUpdate);
        }

        // GET: Repairer/Delete/5
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
            Repairer repairer = db.Repairers.Find(id);
            if (repairer == null)
            {
                return HttpNotFound();
            }
            return View(repairer);
        }

        // POST: Repairer/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Repairer repairer = db.Repairers.Find(id);
                db.Repairers.Remove(repairer);
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