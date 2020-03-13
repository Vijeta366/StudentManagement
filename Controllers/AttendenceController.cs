using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;
using WebApplication5.ViewModel;

namespace WebApplication5.Controllers
{
    public class AttendenceController : Controller
    {
        private StudentDbContext db = new StudentDbContext();

        // GET: Attendence
        public ActionResult Index()
        {
            var attendence = db.Attendence.Include(a => a.Student);
            return View(attendence.ToList());
        }

        // GET: Attendence/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendence attendence = db.Attendence.Find(id);
            if (attendence == null)
            {
                return HttpNotFound();
            }
            return View(attendence);
        }

        // GET: Attendence/Create
        public ActionResult Create()
        {
            ViewBag.StudentId = new SelectList(db.Student, "StudentId", "Name");
            return View();
        }

        // POST: Attendence/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AtendenceId,Date,IsPresent,StudentId")] Attendence attendence)
        {
            if (ModelState.IsValid)
            {
                db.Attendence.Add(attendence);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StudentId = new SelectList(db.Student, "StudentId", "Name", attendence.StudentId);
            return View(attendence);
        }

        // GET: Attendence/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendence attendence = db.Attendence.Find(id);
            if (attendence == null)
            {
                return HttpNotFound();
            }
            ViewBag.StudentId = new SelectList(db.Student, "StudentId", "Name", attendence.StudentId);
            return View(attendence);
        }

        // POST: Attendence/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AtendenceId,Date,IsPresent,StudentId")] Attendence attendence)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attendence).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StudentId = new SelectList(db.Student, "StudentId", "Name", attendence.StudentId);
            return View(attendence);
        }

        // GET: Attendence/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendence attendence = db.Attendence.Find(id);
            if (attendence == null)
            {
                return HttpNotFound();
            }
            return View(attendence);
        }

        // POST: Attendence/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Attendence attendence = db.Attendence.Find(id);
            db.Attendence.Remove(attendence);
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
       //  POST: ShortAttendence
        [HttpPost]
        public ActionResult ShortList(int id)
        {

            var details = (from stu in db.Attendence where stu.StudentId == 5 select stu.StudentId);
            //To find Attendence list ,go through student list,will match classId.
            var StudentList = db.Attendence.Where(i => i.Student.ClassId == id)
                .GroupBy(x => x.StudentId)
                .Select(y => new ShortAttendence
                {
                   StudentId= y.FirstOrDefault().StudentId,
                   No_of_Absence = y.Where(f => f.IsPresent == false).Count(),
                   StudentName= y.FirstOrDefault().Student.Name
                })
                .ToList();
            return View(StudentList);
        }
    }


}
