using AbsenceWebApp.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbsenceWebApp.Models
{
    public class AbsenceRepository : IAbsenceRepository
    {
        private readonly ApplicationDbContext _db;

        public AbsenceRepository(ApplicationDbContext db)
        {
            _db = db;
        }


        public IEnumerable<Absence> GetAll()
        {
            return _db.Absence.Include(u => u.ApplicationUser).ToList();
        }
        public Absence GetById(int id)
        {
            return _db.Absence.Include(u => u.ApplicationUser).Include(u => u.ApplicationUserApprovedBy).FirstOrDefault(i => i.Id == id);
        }
        public IEnumerable<Absence> GetAllAbsencesByUserId(string userId)
        {
            return _db.Absence.Include(u => u.ApplicationUser).Include(u => u.ApplicationUserApprovedBy).Where(i => i.UserID == userId).ToList();
        }
        public IEnumerable<Absence> GetAllAbsencesProvedByUserId(string userId)
        {
            return _db.Absence.Include(u => u.ApplicationUser).Include(u => u.ApplicationUserApprovedBy).Where(i => i.UserID == userId).ToList();
        }
        public void Insert(Absence absence)
        {
            _db.Absence.Add(absence);
        }
        public void Update(Absence absence)
        {
            _db.Update(absence);
        }
        public void Delete(Absence absence)
        {
            _db.Remove(absence);
        }
        public void Save()
        {
            _db.SaveChanges();
        }

    }
}
