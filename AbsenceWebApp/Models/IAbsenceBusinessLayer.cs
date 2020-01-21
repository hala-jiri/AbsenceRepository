using System.Collections.Generic;
using System.Security.Claims;

namespace AbsenceWebApp.Models
{
    public interface IAbsenceBusinessLayer
    {

        IEnumerable<Absence> GetListOfAbsences();

        void CreateAbsence(Absence absence, ClaimsIdentity claimIdentity);
        bool DeleteAbsence(Absence absence, string userId);
        bool DeleteAbsence(Absence absence, ClaimsIdentity claimsIdentity);
        bool DeleteAbsence(int id, string userId);
        bool DeleteAbsence(int id, ClaimsIdentity claimsIdentity);
        IEnumerable<Absence> GetListOfAbsencesByUser(string userId);
        IEnumerable<Absence> GetListOfAbsencesByUser(ClaimsIdentity claimsIdentity);
        Absence GetDetailById(int id, string activeUserId);
        Absence GetDetailById(int id, ClaimsIdentity claimsIdentity);
        bool EditAbsence(int id, Absence absence, string activeUserId);
        bool EditAbsence(int id, Absence absence, ClaimsIdentity claimsIdentity);
        bool ApproveAbsence(int idAbsence, string userIdOfApprove);
        bool ApproveAbsence(int idAbsence, ClaimsIdentity claimsIdentityOfUserOfApprove);
        bool DisapproveAbsence(int idAbsence, string userIdOfApprove);
        bool DisapproveAbsence(int idAbsence, ClaimsIdentity claimsIdentityOfUserOfApprove);

        void LockUser(string userId, ClaimsIdentity claimsIdentity);
        void UnLockUser(string userId, ClaimsIdentity claimsIdentity);
        IEnumerable<ApplicationUser> GetAllUsers();

        IEnumerable<Absence> GetListOfPastAbsences();
        IEnumerable<Absence> GetListOfPastAbsencesByUser(string userId);
        IEnumerable<Absence> GetListOfPastAbsencesByUser(ClaimsIdentity claimsIdentity);
        IEnumerable<Absence> GetListOfUpcomingAbsences();
        IEnumerable<Absence> GetListOfUpcomingAbsencesByUser(string userId);
        IEnumerable<Absence> GetListOfUpcomingAbsencesByUser(ClaimsIdentity claimsIdentity);
        IEnumerable<Absence> GetListOfAbsencesToApprove();
        IEnumerable<Absence> GetListOfAbsencesForApproveByUser(string userId);
        IEnumerable<Absence> GetListOfAbsencesForApproveByUser(ClaimsIdentity claimsIdentity);
        IEnumerable<Absence> GetListOfAbsencesProvedByUser(string userId);
        IEnumerable<Absence> GetListOfAbsencesProvedByUser(ClaimsIdentity claimsIdentity);
    }
}