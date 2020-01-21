using System.Collections.Generic;

namespace AbsenceWebApp.Models
{
    public interface IUserRepository
    {
        IEnumerable<ApplicationUser> GetAll();
    }
}