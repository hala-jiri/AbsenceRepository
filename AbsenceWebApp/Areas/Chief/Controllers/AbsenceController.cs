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

namespace AbsenceWebApp.Areas.Chief.Controllers
{
    // controller for all absences of all users!
    [Area("Chief")]
    [Authorize(Roles = StaticDetails.ManagerUser + "," + StaticDetails.AdminUser)]
    public class AbsenceController : Controller
    {
        private readonly ApplicationDbContext _db;
        public AbsenceController(ApplicationDbContext db)
        {
            _db = db;
        }

        //GET action method
        public async Task<IActionResult> Index()
        {
            return View(await _db.Absence.Include(u => u.ApplicationUser).ToListAsync());
        }

        //GET - Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var absence = await _db.Absence.FindAsync(id);
            if (absence == null)
            {
                return NotFound();
            }
            return View(absence);
        }

        //POST - Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Absence absence)
        {
            if (ModelState.IsValid)
            {
                //TODO: Change model to saving last date of update
                // absence.DatetimeOfCreated = DateTime.Now; can be used for Date of Change

                //TODO: If there will be any changes, absence should be approved or make it not approved.

                //TODO: change this Update for separate attributes updates.
                _db.Update(absence);
                await _db.SaveChangesAsync();

                //TODO: maybe show direct button to Approve it! maybe on detail maybe on edit view

                return RedirectToAction("Index");
            }
            return View(absence);

        }

        //GET - Detail
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var absence = await _db.Absence.FindAsync(id);
            if (absence == null)
            {
                return NotFound();
            }
            return View(absence);
            //TODO: maybe show direct button to Approve it! maybe on detail maybe on edit view
        }

        //GET - Statistics
        public async Task<IActionResult> Statistics()
        {
            //TODO: count hours per one absence (diff) and make a group by
            return View(await _db.Absence.ToListAsync());
        }
    }
}