using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AbsenceWebApp.Data;
using Microsoft.EntityFrameworkCore;
using AbsenceWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using AbsenceWebApp.Utility;
using System.Security.Claims;

namespace AbsenceWebApp.Areas.Chief.Controllers
{
    // controller for all absences of all users!
    [Area("Chief")]
    //[Authorize(Roles = StaticDetails.ManagerUser + "," + StaticDetails.AdminUser)]
    [Authorize(Roles = StaticDetails.ManagerUser)]
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
            return View(_abl.GetListOfAbsences());
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

        ////POST - Edit
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(Absence absence)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //TODO: Change model to saving last date of update
        //        // absence.DatetimeOfCreated = DateTime.Now; can be used for Date of Change

        //        //TODO: If there will be any changes, absence should be approved or make it not approved.

        //        //TODO: change this Update for separate attributes updates.
        //        _db.Update(absence);
        //        await _db.SaveChangesAsync();

        //        //TODO: maybe show direct button to Approve it! maybe on detail maybe on edit view

        //        return RedirectToAction("Index");
        //    }
        //    return View(absence);

        //}

        //GET - Detail
        public IActionResult Detail(int? id)
        {
            if (id != null)
            {
                var absence = _abl.GetDetailById(id.Value, (ClaimsIdentity)this.User.Identity);
                return (absence == null) ? NotFound() : (IActionResult)View(absence);
            }
            return NotFound();
        }

        ////GET - Statistics
        //public async Task<IActionResult> Statistics()
        //{
        //    //TODO: count hours per one absence (diff) and make a group by
        //    return View(await _db.Absence.ToListAsync());
        //}

        // POST 
        public IActionResult Approve(int? id)
        {
            if (id != null)
            {
                return (_abl.ApproveAbsence(id.Value, (ClaimsIdentity)this.User.Identity)) ? (IActionResult)RedirectToAction("Index") : NotFound();
            }
            return NotFound();

        }
        // POST 
        public IActionResult Dissapprove(int? id)
        {
            if (id != null)
            {
                return (_abl.DisapproveAbsence(id.Value, (ClaimsIdentity)this.User.Identity)) ? (IActionResult)RedirectToAction("Index") : NotFound();
            }
            return NotFound();
        }


        //GET action method
        public IActionResult ToApprove()
        {
            return View(_abl.GetListOfAbsencesToApprove());
        }

        //GET action method
        public IActionResult Past()
        {
            return View(_abl.GetListOfPastAbsences());
        }

        //GET action method
        public IActionResult Upcoming()
        {
            return View(_abl.GetListOfUpcomingAbsences());
        }
    }
}