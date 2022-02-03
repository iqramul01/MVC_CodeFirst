using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using _1263087.Models;

namespace _1263087.Controllers
{
    public class PatientController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public JsonResult GetDoctorList(int departmentId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Doctor> doctorList = db.Doctors.Where(d => d.DepartmentID == departmentId).ToList();
            return Json(doctorList, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetDistrictList(int divisionId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<District> districtList = db.Districts.Where(d => d.DivisionID == divisionId).ToList();
            return Json(districtList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUpazilaList(int districtId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Upazila> upazilaList = db.Upazilas.Where(d => d.DistrictID == districtId).ToList();
            return Json(upazilaList, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Index()
        {
            var patients = db.Patients.Include(p => p.BloodGroup).Include(p => p.Department).Include(p => p.District).Include(p => p.Division).Include(p => p.Doctor).Include(p => p.Upazila);
            return View(patients.ToList());
        }



        public ActionResult Create()
        {
            ViewBag.BloodGroupID = new SelectList(db.BloodGroups, "BloodGroupID", "BloodGroupName");
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepartmentName");
            ViewBag.DistrictID = new SelectList(db.Districts, "DistrictID", "DistrictName");
            ViewBag.DivisionID = new SelectList(db.Divisions, "DivisionID", "DivisionName");
            ViewBag.DoctorID = new SelectList(db.Doctors, "DoctorID", "DoctorName");
            ViewBag.UpazilaID = new SelectList(db.Upazilas, "UpazilaID", "UpazilaName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "PatientID,PatientName,EmergencyContact,BloodGroupID,DivisionID,DistrictID,UpazilaID,DepartmentID,DoctorID")] Patient patient)
        public ActionResult Create(Patient patient)

        {
            if (ModelState.IsValid)
            {
                db.Patients.Add(patient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BloodGroupID = new SelectList(db.BloodGroups, "BloodGroupID", "BloodGroupName", patient.BloodGroupID);
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepartmentName", patient.DepartmentID);
            ViewBag.DistrictID = new SelectList(db.Districts, "DistrictID", "DistrictName", patient.DistrictID);
            ViewBag.DivisionID = new SelectList(db.Divisions, "DivisionID", "DivisionName", patient.DivisionID);
            ViewBag.DoctorID = new SelectList(db.Doctors, "DoctorID", "DoctorName", patient.DoctorID);
            ViewBag.UpazilaID = new SelectList(db.Upazilas, "UpazilaID", "UpazilaName", patient.UpazilaID);
            return View(patient);
        }

        public ActionResult Edit(int id=0)
        {

            Patient patient = db.Patients.Find(id);

            if (patient == null)
            {
                return HttpNotFound();
            }
            ViewBag.BloodGroupID = new SelectList(db.BloodGroups, "BloodGroupID", "BloodGroupName", patient.BloodGroupID);
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepartmentName", patient.DepartmentID);
            ViewBag.DistrictID = new SelectList(db.Districts, "DistrictID", "DistrictName", patient.DistrictID);
            ViewBag.DivisionID = new SelectList(db.Divisions, "DivisionID", "DivisionName", patient.DivisionID);
            ViewBag.DoctorID = new SelectList(db.Doctors, "DoctorID", "DoctorName", patient.DoctorID);
            ViewBag.UpazilaID = new SelectList(db.Upazilas, "UpazilaID", "UpazilaName", patient.UpazilaID);
            return View(patient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BloodGroupID = new SelectList(db.BloodGroups, "BloodGroupID", "BloodGroupName", patient.BloodGroupID);
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepartmentName", patient.DepartmentID);
            ViewBag.DistrictID = new SelectList(db.Districts, "DistrictID", "DistrictName", patient.DistrictID);
            ViewBag.DivisionID = new SelectList(db.Divisions, "DivisionID", "DivisionName", patient.DivisionID);
            ViewBag.DoctorID = new SelectList(db.Doctors, "DoctorID", "DoctorName", patient.DoctorID);
            ViewBag.UpazilaID = new SelectList(db.Upazilas, "UpazilaID", "UpazilaName", patient.UpazilaID);
            return View(patient);
        }


        public ActionResult Delete(int id=0)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Patient patient = db.Patients.Find(id);
            //if (patient == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(patient);
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id = 0)
        {
            Patient patient = db.Patients.Find(id);
            db.Patients.Remove(patient);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id = 0)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Patient patient = db.Patients.Find(id);
            //if (patient == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(patient);
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
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
