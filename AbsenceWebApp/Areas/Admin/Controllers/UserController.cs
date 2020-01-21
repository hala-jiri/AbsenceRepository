using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AbsenceWebApp.Data;
using AbsenceWebApp.Models;
using AbsenceWebApp.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AbsenceWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = StaticDetails.AdminUser)]
    public class UserController : Controller
    {
        private readonly IAbsenceBusinessLayer _abl;
        public UserController(IAbsenceBusinessLayer abl)
        {
            _abl = abl;
        }

        //GET
        public IActionResult Index()
        {
            return View(_abl.GetAllUsers());
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LockUser(string id)
        {
            if(id != null)
            {
                _abl.LockUser(id, (ClaimsIdentity)this.User.Identity);
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UnLockUser(string id)
        {
            if (id != null)
            {
                _abl.UnLockUser(id, (ClaimsIdentity)this.User.Identity);
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}