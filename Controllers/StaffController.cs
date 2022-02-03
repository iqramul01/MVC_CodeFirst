
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using _1263087.Models;
using System.Data.Entity;

namespace _1263087.Controllers
{
    public class StaffController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Staff
        public ActionResult Index()
        {
            return View(db.Staffs.ToList());
        }

        public ActionResult Add()
        {
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepartmentName");
            return PartialView();
        }
        [HttpPost]
        public ActionResult Add(Staff staff)
        {
            if (ModelState.IsValid)
            {
                db.Staffs.Add(staff);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return PartialView("_StaffInfo", db.Staffs.ToList());
        }


        public ActionResult Edit(int id)
        {

            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepartmentName");
            return PartialView(db.Staffs.Find(id));

        }
        [HttpPost]
        public ActionResult Edit(Staff staff, int id)
        {
            if (ModelState.IsValid)
            {
                db.Entry(staff).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ///*return RedirectToAction("Index"); /*//
            return PartialView("_StaffInfo", db.Staffs.ToList());
        }

        public ActionResult Delete(int id)
        {
            db.Staffs.Remove(db.Staffs.Find(id));
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}