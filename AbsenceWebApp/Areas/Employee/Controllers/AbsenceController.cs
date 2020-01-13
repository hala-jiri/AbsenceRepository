﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AbsenceWebApp.Data;
using Microsoft.EntityFrameworkCore;
using AbsenceWebApp.Models;

namespace AbsenceWebApp.Areas.Employee.Controllers
{
    [Area("Employee")]
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
            return View(await _db.Absence.ToListAsync());
        }


        // Get - Create
        public IActionResult Create()
        {
            return View();
        }

        //POST - Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Absence absence)
        {
            if (ModelState.IsValid)
            {
                absence.DatetimeOfCreated = DateTime.Now;
                // if is model valid
                _db.Absence.Add(absence);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(absence);

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

                return RedirectToAction("Index");
            }
            return View(absence);

        }
    }
}