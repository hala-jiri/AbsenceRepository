using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AbsenceWebApp.Data;
using Microsoft.EntityFrameworkCore;
using AbsenceWebApp.Models;
using AbsenceWebApp.Utility;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace AbsenceWebApp.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize(Roles = StaticDetails.EmployeeUser + "," + StaticDetails.ManagerUser + "," + StaticDetails.AdminUser)]
    public class AbsenceController : Controller
    {
        private readonly IAbsenceBusinessLayer _abl;
        public AbsenceController(IAbsenceBusinessLayer abl)
        {
            _abl = abl;
        }

        //GET action method
        public IActionResult Index()
        {
            return View(_abl.GetListOfAbsencesByUser((ClaimsIdentity)this.User.Identity));
        }


        // Get - Create
        public IActionResult Create()
        {
            return View();
        }

        //POST - Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Absence absence)
        {
            if (ModelState.IsValid && absence.DatetimeFrom < absence.DatetimeTo)
            {
                _abl.CreateAbsence(absence, (ClaimsIdentity)this.User.Identity);
                return RedirectToAction("Index");
            }
            return View(absence);
        }

        //GET - Edit
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                var absence = _abl.GetDetailById(id.Value, (ClaimsIdentity)this.User.Identity);
                return (absence == null) ? NotFound() : (IActionResult)View(absence);
            }
            return NotFound();
        }

        //POST - Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Absence absence)
        {
            if (ModelState.IsValid)
            {
                _abl.EditAbsence(absence.Id, absence, (ClaimsIdentity)this.User.Identity);
                return RedirectToAction("Index");
            }
            return View(absence);

        }


        //GET - Delete
        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                var absence = _abl.GetDetailById(id.Value, (ClaimsIdentity)this.User.Identity);
                return (absence == null) ? NotFound() : (IActionResult)View(absence);
            }
            return NotFound();
        }


        //POST - Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            return (_abl.DeleteAbsence(id, (ClaimsIdentity)this.User.Identity)) ? (IActionResult)RedirectToAction("Index") : NotFound();
        }

        //GET - Detail
        public IActionResult Detail(int? id)
        {
            if(id != null)
            {
                var absence = _abl.GetDetailById(id.Value, (ClaimsIdentity)this.User.Identity);
                return (absence == null) ? NotFound() : (IActionResult)View(absence);
            }
            return NotFound();
        }
    }
}