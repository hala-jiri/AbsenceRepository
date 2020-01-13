using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AbsenceWebApp.Models
{
    public class Absence
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }
        [Required]
        public DateTime DatetimeFrom { get; set; }
        [Required]
        public DateTime DatetimeTo{ get; set; }
        public string Reason { get; set; }
        public DateTime DatetimeOfCreated { get; set; }
        public bool Approved { get; set; }
        public DateTime ApprovedDate { get; set; }
        public string ApprovedByUserName { get; set; }
    }
}
