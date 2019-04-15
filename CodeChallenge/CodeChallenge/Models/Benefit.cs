using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeChallenge.Models
{
    public class Benefit
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BenefitID { get; set; }
        public string Coverage { get; set; }
        public float CostofBenefit { get; set; }
        public string DiscountLetter { get; set; }
        public float Discount { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}