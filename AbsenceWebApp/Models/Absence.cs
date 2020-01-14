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

        [Display(Name = "Employee")]
        [Required]
        public string UserName { get; set; }

        [Display(Name ="Absence from")]
        [Required]
        public DateTime DatetimeFrom { get; set; }

        [Display(Name = "Absence till")]
        [Required]
        public DateTime DatetimeTo{ get; set; }

        [Display(Name = "Reason")]
        public string Reason { get; set; }

        [Display(Name = "Created")]
        public DateTime DatetimeOfCreated { get; set; }

        [Display(Name = "Approved")]
        public bool Approved { get; set; }

        [Display(Name = "Approved date")]
        public DateTime ApprovedDate { get; set; }

        [Display(Name = "Approved by")]
        public string ApprovedByUserName { get; set; }
    }
}
