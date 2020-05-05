using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EngeesCollege.Models;

namespace EngeesCollege.Controllers
{
    public class DepartmentController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: Department
        public ActionResult Index()
        {
            var departments = db.Departments.Include(d => d.Administrator).Include(d => d.Faculty);
            return View(departments.ToList());
        }

        // GET: Department/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // GET: Department/Create
        public ActionResult Create()
        {
            ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "LastName");
            ViewBag.FacultyId = new SelectList(db.Faculties, "FacultyId", "Name");
            return View();
        }

        // POST: Department/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DepartmentId,Name,StartDate,FacultyId,InstructorID")] Department department)
        {
            if (ModelState.IsValid)
            {
                department.StartDate = DateTime.Now;
                db.Departments.Add(department);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "LastName", department.InstructorID);
            ViewBag.FacultyId = new SelectList(db.Faculties, "FacultyId", "Name", department.FacultyId);
            return View(department);
        }

        // GET: Department/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "LastName", department.InstructorID);
            ViewBag.FacultyId = new SelectList(db.Faculties, "FacultyId", "Name", department.FacultyId);
            return View(department);
        }

        // POST: Department/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DepartmentId,Name,StartDate,FacultyId,InstructorID")] Department department)
        {
            if (ModelState.IsValid)
            {
                db.Entry(department).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "LastName", department.InstructorID);
            ViewBag.FacultyId = new SelectList(db.Faculties, "FacultyId", "Name", department.FacultyId);
            return View(department);
        }
        
        // GET: Department/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Department/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Department department = db.Departments.Find(id);
            db.Departments.Remove(department);
            db.SaveChanges();
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
