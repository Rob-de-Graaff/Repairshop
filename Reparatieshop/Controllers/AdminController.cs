﻿using Microsoft.AspNet.Identity.Owin;
using Reparatieshop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Reparatieshop.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private ApplicationRoleManager _roleManager;

        public AdminController()
        {
        }

        public AdminController(ApplicationRoleManager roleManager)
        {
            RoleManager = roleManager;
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        // GET: Admin
        public ActionResult Index()
        {
            List<AdminViewModel> listRoles = new List<AdminViewModel>();
            foreach (ApplicationRole role in RoleManager.Roles)
            {
                listRoles.Add(new AdminViewModel(role));
            }
            return View(listRoles);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(AdminViewModel model)
        {
            ApplicationRole role = new ApplicationRole() { Name = model.Name };
            await RoleManager.CreateAsync(role);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Edit(string id)
        {
            ApplicationRole role = await RoleManager.FindByIdAsync(id);
            return View(new AdminViewModel(role));
        }

        [HttpPost]
        public async Task<ActionResult> Edit(AdminViewModel model)
        {
            ApplicationRole role = new ApplicationRole() { Id = model.Id, Name = model.Name };
            await RoleManager.UpdateAsync(role);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Details(string id)
        {
            ApplicationRole role = await RoleManager.FindByIdAsync(id);
            return View(new AdminViewModel(role));
        }

        public async Task<ActionResult> Delete(string id)
        {
            ApplicationRole role = await RoleManager.FindByIdAsync(id);
            return View(new AdminViewModel(role));
        }

        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            ApplicationRole role = await RoleManager.FindByIdAsync(id);
            await RoleManager.DeleteAsync(role);
            return RedirectToAction("Index");
        }
    }
}