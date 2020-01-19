using System.Collections.Generic;

namespace AbsenceWebApp.Models
{
    public interface IAbsenceRepository
    {

        IEnumerable<Absence> GetAll();
        IEnumerable<Absence> GetAllAbsencesByUserId(string userId); 
        IEnumerable<Absence> GetAllAbsencesProvedByUserId(string userId); 
        Absence GetById(int id);
        void Insert(Absence absence);
        void Update(Absence absence);
        void Delete(Absence absence);
        void Save();
    }
}