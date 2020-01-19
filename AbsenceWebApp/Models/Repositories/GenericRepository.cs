using AbsenceWebApp.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbsenceWebApp.Models
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;

        private DbSet<T> table = null;
        public GenericRepository()
        {
            //this._db = new ApplicationDbContext();
            table = _db.Set<T>();
        }
        public GenericRepository(ApplicationDbContext db)
        {
            this._db = db;
            table = _db.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }
        public T GetById(object id)
        {
            return table.Find(id);
        }
        public void Insert(T obj)
        {
            table.Add(obj);
        }
        public void Update(T obj)
        {
            table.Attach(obj);
            _db.Entry(obj).State = EntityState.Modified;
        }
        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
