using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CodeChallenge.DAL;
using CodeChallenge.Models;


namespace CodeChallenge.Controllers
{
    public class EnrollmentController : Controller
    {
        private BenefitContext db = new BenefitContext();

        // GET: Enrollment
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.DateSortParm = sortOrder;
            var enrollments = from s in db.Enrollments
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                enrollments = enrollments.Where(s => s.LastName.Contains(searchString)
                               || s.FirstName.Contains(searchString)
                               || s.Employee.LastName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    enrollments = enrollments.OrderByDescending(s => s.LastName);
                    break;
                default:
                    enrollments = enrollments.OrderBy(s => s.LastName);
                    break;
            }
            return View(enrollments.ToList());
        }

        // GET: Enrollment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // GET: Enrollment/Create
        public ActionResult Create()
        {
            int id = 0;
            if (Session["EmployeeID"] != null)
            {
                id = (int)Session["EmployeeID"];

                // cast it and use it
                // The code
            }
            System.Console.WriteLine("LastName = " + id);
            ViewBag.BenefitID = new SelectList(db.Benefits, "BenefitID", "Coverage");
            if (id != 0)
            {
                ViewBag.EmployeeID = new SelectList(db.Employees, "ID", "LastName", id);
            }
            else
            {
                ViewBag.EmployeeID = new SelectList(db.Employees, "ID", "LastName");
            }
            return View();
        }

        // POST: Enrollment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EnrollmentID,BenefitID,EmployeeID,LastName,FirstName,DependentType")] Enrollment enrollment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Enrollments.Add(enrollment);
                    db.SaveChanges();
                    return RedirectToAction("Details", "Employee", new { id = enrollment.EmployeeID });
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            ViewBag.BenefitID = new SelectList(db.Benefits, "BenefitID", "Coverage", enrollment.BenefitID);
            ViewBag.EmployeeID = new SelectList(db.Employees, "ID", "LastName", enrollment.EmployeeID);
            return View(enrollment);
        }

        // GET: Enrollment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            
            ViewBag.BenefitID = new SelectList(db.Benefits, "BenefitID", "Coverage", enrollment.BenefitID);
            ViewBag.EmployeeID = new SelectList(db.Employees, "ID", "LastName", enrollment.EmployeeID);

            //Session["EmployeeID"] = null; //sws

            return View(enrollment);
        }

        // POST: Enrollment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EnrollmentID,BenefitID,EmployeeID,LastName,FirstName,DependentType")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enrollment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BenefitID = new SelectList(db.Benefits, "BenefitID", "Coverage", enrollment.BenefitID);
            ViewBag.EmployeeID = new SelectList(db.Employees, "ID", "LastName", enrollment.EmployeeID);
            return View(enrollment);
        }

        // GET: Enrollment/Delete/5
        //public ActionResult Delete(int? id)
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }

            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // POST: Enrollment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Enrollment enrollment = db.Enrollments.Find(id);
            try
            {
                db.Enrollments.Remove(enrollment);
                db.SaveChanges();
            }
            catch (DataException/* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
