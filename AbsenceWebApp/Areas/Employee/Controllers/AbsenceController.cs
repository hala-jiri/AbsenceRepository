using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AbsenceWebApp.Data;
using Microsoft.EntityFrameworkCore;

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
    }
}