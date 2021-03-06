﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EngeesCollege.Models;
using EngeesCollege.ViewModels;

namespace EngeesCollege.Controllers
{
    public class InstructorController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: Instructor
        public ActionResult Index(int? id, int? courseID)
        {
            var viewModel = new InstructorIndexData();
            viewModel.Instructors = db.Instructors
                .Include(i => i.OfficeAssignment)
                .Include(i => i.Courses.Select(c => c.Department)).OrderBy(i => i.LastName);         
 

    if (id != null) { ViewBag.InstructorID = id.Value;
                viewModel.Courses = viewModel.Instructors.Where(i => i.ID == id.Value).Single().Courses; }

            if (courseID != null) { 
                ViewBag.CourseID = courseID.Value;
                viewModel.Enrollments = viewModel.Courses.Where(x => x.CourseID == courseID).Single().Enrollments;
            }

            return View(viewModel);
        }

        // GET: Instructor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructor instructor = db.Instructors.Find(id);
            if (instructor == null)
            {
                return HttpNotFound();
            }
            return View(instructor);
        }

        // GET: Instructor/Create
        public ActionResult Create()
        {
            ViewBag.ID = new SelectList(db.OfficeAssignments, "InstructorID", "Location");
            return View();
        }

        // POST: Instructor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,LastName,FirstName,Email,Salary,Qualification,MaritalStatus,Gender,HireDate,DoB,Age")] Instructor instructor)
        {
            var age = DateTime.Now.Year - instructor.DoB.Year;
            if (ModelState.IsValid)
            {
                instructor.HireDate = DateTime.Now;
                instructor.Age = age;
                db.Instructors.Add(instructor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID = new SelectList(db.OfficeAssignments, "InstructorID", "Location", instructor.ID);
            return View(instructor);
        }

        // GET: Instructor/Edit/5
        [HttpGet]
        public ActionResult Edit(int? id) { 
            if (id == null) { 
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            Instructor instructor = db.Instructors
                .Include(i => i.OfficeAssignment)
                .Include(i => i.Courses)
                .Where(i => i.ID == id)
                .Single(); 
            PopulateAssignedCourseData(instructor);
            if (instructor == null) { 
                return HttpNotFound(); 
            }
            return View(instructor); 
        }

        private void PopulateAssignedCourseData(Instructor instructor)
        {
            var allCourses = db.Courses;
            var instructorCourses = new HashSet<int>(instructor.Courses.Select(c => c.CourseID));
            var viewModel = new List<AssignedCourseData>();
            foreach (var course in allCourses)
            {
                viewModel.Add(new AssignedCourseData{

            CourseID = course.CourseID,   
            Title = course.Title,   
            Assigned = instructorCourses.Contains(course.CourseID)  });
            }
            ViewBag.Courses = viewModel;
        }




        private void UpdateInstructorCourses(string[] selectedCourses, Instructor instructorToUpdate) {
            if (selectedCourses == null) { instructorToUpdate.Courses = new List<Course>();
                return;
            } var selectedCoursesHS = new HashSet<string>(selectedCourses); 
            var instructorCourses = new HashSet<int>(instructorToUpdate.Courses.Select(c => c.CourseID));
            foreach (var course in db.Courses) { 
                if (selectedCoursesHS.Contains(course.CourseID.ToString())) { 
                    if (!instructorCourses.Contains(course.CourseID)) { 
                        instructorToUpdate.Courses.Add(course); } } else { 
                    if (instructorCourses.Contains(course.CourseID)) { instructorToUpdate.Courses.Remove(course); 
                    }
                }
            } 
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string[] selectedCourses)
        {
            if (id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            var instructorToUpdate = db.Instructors.Include(i => i.OfficeAssignment).Include(i => i.Courses).Where(i => i.ID == id).Single();

            if (TryUpdateModel(instructorToUpdate, "", new string[] { "LastName", "FirstName", "HireDate", "OfficeAssignment" }))
            {
                try
                {
                    if (String.IsNullOrWhiteSpace(instructorToUpdate.OfficeAssignment.Location)) { instructorToUpdate.OfficeAssignment = null; }

                    UpdateInstructorCourses(selectedCourses, instructorToUpdate);
                    db.Entry(instructorToUpdate).State = EntityState.Modified;



                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {             //Log the error (uncomment dex variable name and add a line here to write a log.             ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");         }     }     PopulateAssignedCourseData(instructorToUpdate);     return View(instructorToUpdate); } private void UpdateInstructorCourses(string[] selectedCourses, Instructor instructorToUpdate) {    if (selectedCourses == null)    {       instructorToUpdate.Courses = new List<Course>();       return;    }      var selectedCoursesHS = new HashSet<string>(selectedCourses);    var instructorCourses = new HashSet<int>        (instructorToUpdate.Courses.Select(c => c.CourseID));    foreach (var course in db.Courses)    {       if (selectedCoursesHS.Contains(course.CourseID.ToString()))       {          if (!instructorCourses.Contains(course.CourseID))          {             instructorToUpdate.Courses.Add(course);          }       }       else       {          if (instructorCourses.Contains(course.CourseID))          {             instructorToUpdate.Courses.Remove(course);          }       }    } } 
                }
            }
            return View(instructorToUpdate);
        }
                    /*
                    [HttpPost]
                                [ValidateAntiForgeryToken]
                                public ActionResult Edit([Bind(Include = "ID,LastName,FirstName,Email,Salary,Qualification,MaritalStatus,Gender,HireDate,DoB,Age")]int id, Instructor instructor)
                                {
                                    var Dbinstructor = db.Instructors.Include(i => i.OfficeAssignment).Where(i => i.ID == id).Single();
                        //Instructor Dbinstructor = db.Instructors.Find(id);

                        if (ModelState.IsValid)
                        {
                            Dbinstructor.Age = Dbinstructor.Age;
                            Dbinstructor.DoB = Dbinstructor.DoB;
                            Dbinstructor.Gender = Dbinstructor.Gender;
                            Dbinstructor.HireDate = Dbinstructor.HireDate;
                            Dbinstructor.FirstName = instructor.FirstName;
                            Dbinstructor.LastName = instructor.LastName;
                            Dbinstructor.Email = instructor.Email;
                            Dbinstructor.Salary = instructor.Salary;
                            Dbinstructor.Qualification = instructor.Qualification;
                            Dbinstructor.MaritalStatus = instructor.MaritalStatus;
                            if (TryUpdateModel(instructor, "", new string[] { "LastName", "FirstMidName", "HireDate", "OfficeAssignment" }))
                            {


                                if (String.IsNullOrWhiteSpace(instructor.OfficeAssignment.Location)) { instructor.OfficeAssignment = null; }

                                db.Entry(instructor).State = EntityState.Modified; 
                                db.SaveChanges();                    
                                return RedirectToAction("Index");
                            }
                        }
                                    ViewBag.ID = new SelectList(db.OfficeAssignments, "InstructorID", "Location", instructor.ID);
                                    return View(instructor);
                                }
                                */



                    // GET: Instructor/Delete/5
                    public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructor instructor = db.Instructors.Find(id);
            if (instructor == null)
            {
                return HttpNotFound();
            }
            return View(instructor);
        }

        // POST: Instructor/Delete/5
       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Instructor instructor = db.Instructors
                .Include(i => i.OfficeAssignment)
                .Where(i => i.ID == id).Single();

            instructor.OfficeAssignment = null;
            db.Instructors.Remove(instructor);

            var department = db.Departments
                .Where(d => d.InstructorID == id)
                .SingleOrDefault(); 
            if (department != null) { department.InstructorID = null; }

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
