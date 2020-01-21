using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AbsenceWebApp.Data;
using AbsenceWebApp.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection; // is it necessary?

namespace AbsenceWebApp.Models
{
    public class AbsenceBusinessLayer : IAbsenceBusinessLayer
    {
        private readonly IAbsenceRepository _absenceRepository;
        private readonly IUserRepository _userRepository;

        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AbsenceBusinessLayer(IAbsenceRepository absRepository, IUserRepository usrRepository, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _absenceRepository = absRepository;
            _userRepository = usrRepository;
        }


        public IEnumerable<Absence> GetListOfAbsences()
        {
            return _absenceRepository.GetAll();
        }

        public void CreateAbsence(Absence absence, ClaimsIdentity claimsIdentity)
        {
            string userId = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;
            if (userId != null)
            {
                absence.UserID = userId;
                // check if there is already absence which cover
                // take userID and Find all his absences which are in that area
                var coverAbsences = _absenceRepository.GetAll()
                                                      .Where(u => u.UserID == absence.UserID)
                                                      .Where(a => a.DatetimeTo >= absence.DatetimeFrom && a.DatetimeFrom <= absence.DatetimeTo).ToList();
                if (coverAbsences == null || coverAbsences.Count == 0)
                {
                    _absenceRepository.Insert(absence);
                    _absenceRepository.Save();
                }
            }
            
        }

        public bool DeleteAbsence(Absence absence, string userId)
        {
            if(absence.UserID == userId)
            { 
                _absenceRepository.Delete(absence);
                _absenceRepository.Save();
                return true;
            }
            return false;
        }
        public bool DeleteAbsence(Absence absence, ClaimsIdentity claimsIdentity)
        {
            return DeleteAbsence(absence, claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);
        }
        public bool DeleteAbsence(int id, string userId)
        {
            var absence = GetDetailById(id, userId);
            if (absence != null)
            {
                _absenceRepository.Delete(absence);
                _absenceRepository.Save();
                return true;
            }
            return false;
        }
        public bool DeleteAbsence(int id, ClaimsIdentity claimsIdentity)
        {
            return DeleteAbsence(id, claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);
        }

        public IEnumerable<Absence> GetListOfAbsencesByUser(string userId)
        {
            return _absenceRepository.GetAll().Where(u => u.UserID == userId);
        }
        public IEnumerable<Absence> GetListOfAbsencesByUser(ClaimsIdentity claimsIdentity)
        {
            return GetListOfAbsencesByUser(claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);
        }
        public Absence GetDetailById(int id, string activeUserId)
        {
            /// Detail of absence is allowed just to owner of absence or to Manager or Admin
           
            var absence = _absenceRepository.GetById(id);
            return (absence.UserID == activeUserId || 
                    IsUserInRole(activeUserId, StaticDetails.ManagerUser) ||
                    IsUserInRole(activeUserId, StaticDetails.AdminUser)) ? absence : null;
        }
        public Absence GetDetailById(int id, ClaimsIdentity claimsIdentity)
        {
            return GetDetailById(id, claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);
        }
        public bool EditAbsence(int id, Absence absence, string activeUserId)
        {
            /// Edit absence is allowed just for owner of absence

            var absenceLoaded = _absenceRepository.GetById(id);
            if (absenceLoaded != null &&
                absenceLoaded.Id == absence.Id &&
                absenceLoaded.UserID == activeUserId)
            {
                if (absence.Reason != null && absence.Reason != absenceLoaded.Reason)
                    absenceLoaded.Reason = absence.Reason;
                if (absence.DatetimeFrom != null && absence.DatetimeFrom != absenceLoaded.DatetimeFrom)
                    absenceLoaded.DatetimeFrom = absence.DatetimeFrom;
                if (absence.DatetimeTo != null && absence.DatetimeTo != absenceLoaded.DatetimeTo)
                    absenceLoaded.DatetimeTo = absence.DatetimeTo;

                absenceLoaded.Approved = null;
                absenceLoaded.ApprovedByUserID = null;
                absenceLoaded.ApprovedDate = null;

                _absenceRepository.Save();
                return true;
            }
            else
            {   // not found any absence or there is different ID of absence or not be owner of that absence
                return false;
            }

        }
        public bool EditAbsence(int id, Absence absence, ClaimsIdentity claimsIdentity)
        {
            return EditAbsence(id, absence, claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);
        }
        public bool ApproveAbsence(int idAbsence, string userIdOfApprove)
        {
            var absence = _absenceRepository.GetById(idAbsence);
            return (absence != null && IsUserInRole(userIdOfApprove, StaticDetails.ManagerUser)) ? ChangeStatusOfApprove(absence, userIdOfApprove, true) : false;
        }
        public bool ApproveAbsence(int idAbsence, ClaimsIdentity claimsIdentityOfUserOfApprove)
        {
            return ApproveAbsence(idAbsence, claimsIdentityOfUserOfApprove.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);
        }
        public bool DisapproveAbsence(int idAbsence, string userIdOfApprove)
        {
            var absence = _absenceRepository.GetById(idAbsence);
            return (absence != null && IsUserInRole(userIdOfApprove, StaticDetails.ManagerUser)) ? ChangeStatusOfApprove(absence, userIdOfApprove, false) : false;
        }
        public bool DisapproveAbsence(int idAbsence, ClaimsIdentity claimsIdentityOfUserOfApprove)
        {
            return DisapproveAbsence(idAbsence, claimsIdentityOfUserOfApprove.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);
        }

        public IEnumerable<Absence> GetListOfPastAbsences()
        {
            return _absenceRepository.GetAll().Where(u => u.DatetimeFrom <= DateTime.Now);
        }
        public IEnumerable<Absence> GetListOfPastAbsencesByUser(string userId)
        {
            return _absenceRepository.GetAllAbsencesByUserId(userId).Where(u => u.DatetimeFrom <= DateTime.Now);
        }
        public IEnumerable<Absence> GetListOfPastAbsencesByUser(ClaimsIdentity claimsIdentity)
        {
            return GetListOfPastAbsencesByUser(claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);
        }
        public IEnumerable<Absence> GetListOfUpcomingAbsences()
        {
            return _absenceRepository.GetAll().Where(u => u.DatetimeFrom >= DateTime.Now);
        }
        public IEnumerable<Absence> GetListOfUpcomingAbsencesByUser(string userId)
        {
            return _absenceRepository.GetAllAbsencesByUserId(userId).Where(u => u.DatetimeFrom >= DateTime.Now);
        }
        public IEnumerable<Absence> GetListOfUpcomingAbsencesByUser(ClaimsIdentity claimsIdentity)
        {
            return GetListOfUpcomingAbsencesByUser(claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);
        }
        public IEnumerable<Absence> GetListOfAbsencesToApprove()
        {
            return _absenceRepository.GetAll().Where(a => a.Approved == null);
        }
        public IEnumerable<Absence> GetListOfAbsencesForApproveByUser(string userId)
        {
            return _absenceRepository.GetAllAbsencesByUserId(userId).Where(a => a.Approved == null && a.UserID == userId);
        }
        public IEnumerable<Absence> GetListOfAbsencesForApproveByUser(ClaimsIdentity claimsIdentity)
        {
            return GetListOfAbsencesForApproveByUser(claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);
        }
        public IEnumerable<Absence> GetListOfAbsencesProvedByUser(string userId)
        {
            return _absenceRepository.GetAllAbsencesProvedByUserId(userId);
        }
        public IEnumerable<Absence> GetListOfAbsencesProvedByUser(ClaimsIdentity claimsIdentity)
        {
            return GetListOfAbsencesProvedByUser(claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);
        }



        
        // USERS PART
        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            return _userRepository.GetAll();
        }

        public void LockUser(string userId, ClaimsIdentity claimsIdentity)
        {
            ChangeLockStatusOfUser(userId, claimsIdentity, true);
        }
        public void UnLockUser(string userId, ClaimsIdentity claimsIdentity)
        {
            ChangeLockStatusOfUser(userId, claimsIdentity, false);
        }

        // PRIVATE
        private bool ChangeStatusOfApprove(Absence absence, string userIdOfApprove, bool changeOnStatus)
        {
            if (absence.Approved != changeOnStatus)
            {
                absence.Approved = changeOnStatus;
                absence.ApprovedByUserID = userIdOfApprove;
                absence.ApprovedDate = DateTime.Now;

                _absenceRepository.Update(absence);
                _absenceRepository.Save();
                return true;
            }
            return false;
        }
        private bool IsUserInRole(string userId, string roleName)
        {
            var userOfApprove = _userManager.FindByIdAsync(userId);
            userOfApprove.Wait();

            var isInRole = _userManager.IsInRoleAsync(userOfApprove.Result, roleName);
            isInRole.Wait();

            return isInRole.Result;
        }
        private void ChangeLockStatusOfUser(string userId, ClaimsIdentity claimsIdentity, bool lockStatus)
        {

            string userIdOfAdmin = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;
            if (IsUserInRole(userIdOfAdmin, StaticDetails.AdminUser) && userId != userIdOfAdmin)
            {
                // user is admin and he is not changing status to himself
                var userById = _userManager.FindByIdAsync(userId);
                userById.Wait();

                if (lockStatus)
                    userById.Result.LockoutEnd = DateTime.Now.AddDays(30);
                else
                    userById.Result.LockoutEnd = null;

            }
        }

    }
}
