using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeChallenge.Models
{
    public enum DependentType
    {
       EMPLOYEE, SPOUSE, CHILD
    }

    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int BenefitID { get; set; }
        public int EmployeeID { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        public DependentType? DependentType { get; set; }
        public string Secret { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Benefit Benefit { get; set; }
    }
}