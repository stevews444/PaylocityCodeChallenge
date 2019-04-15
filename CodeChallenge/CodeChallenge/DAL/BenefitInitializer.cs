using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using CodeChallenge.Models;

namespace CodeChallenge.DAL
{
    public class BenefitInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<BenefitContext>
    {
        protected override void Seed(BenefitContext context)
        {
            var employees = new List<Employee>
            {
            new Employee{FirstName="Carson",LastName="Alexander",PayCheckAmt=2000},
            new Employee{FirstName="Meredith",LastName="Manso",PayCheckAmt=2000},
            new Employee{FirstName="Micky",LastName="Mouse",PayCheckAmt=2000}
            };

            employees.ForEach(s => context.Employees.Add(s));
            context.SaveChanges();

            var benefits = new List<Benefit>
            {
            new Benefit{BenefitID=1050,Coverage="BlueCross",CostofBenefit=1000,DiscountLetter="A",Discount=10},
            new Benefit{BenefitID=4022,Coverage="BlueCross",CostofBenefit=500,DiscountLetter="A",Discount=10}
            };
            benefits.ForEach(s => context.Benefits.Add(s));
            context.SaveChanges();

            var enrollments = new List<Enrollment>
            {
            new Enrollment{EnrollmentID=1,BenefitID=1050,EmployeeID=1,FirstName="Carson",LastName="Alexander",DependentType=DependentType.EMPLOYEE},
            new Enrollment{EnrollmentID=1,BenefitID=4022,EmployeeID=1,FirstName="Julie",LastName="Alexander",DependentType=DependentType.SPOUSE},
            new Enrollment{EnrollmentID=1,BenefitID=4022,EmployeeID=1,FirstName="Sonny",LastName="Alexander",DependentType=DependentType.CHILD},
            new Enrollment{EnrollmentID=2,BenefitID=1050,EmployeeID=2,FirstName="Meredith",LastName="Manso",DependentType=DependentType.EMPLOYEE},
            new Enrollment{EnrollmentID=3,BenefitID=1050,EmployeeID=3,FirstName="Micky",LastName="Mouse",DependentType=DependentType.EMPLOYEE},
            new Enrollment{EnrollmentID=3,BenefitID=4022,EmployeeID=3,FirstName="Marsha",LastName="Mouse",DependentType=DependentType.SPOUSE}
            };
            enrollments.ForEach(s => context.Enrollments.Add(s));
            context.SaveChanges();
        }
    }
}