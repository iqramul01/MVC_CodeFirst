using _1263087.Models;
using _1263087.Models.DAL.Interface;
using _1263087.Models.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _1263087.Controllers
{
    public class AdmitController : Controller
    {
        private IGenericRepository<Admit> _db;
        private IGenericRepository<Department> _dbDep;
        private IGenericRepository<Patient> _dbPat;

        public AdmitController()
        {
            this._db = new GenericRepository<Admit>(new ApplicationDbContext());
            this._dbDep = new GenericRepository<Department>(new ApplicationDbContext());
            this._dbPat = new GenericRepository<Patient>(new ApplicationDbContext());

        }
      
        // GET: Course
        public ActionResult Index()
        {
            var admit = _db.GetAll();
            return View(admit);
        }
        public ActionResult Create()
        {
            ViewBag.DepartmentID = new SelectList(_dbDep.GetAll(), "DepartmentID", "DepartmentName");
            ViewBag.PatientID = new SelectList(_dbPat.GetAll(), "PatientID", "PatientName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Admit admit)
        {
            if (ModelState.IsValid)
            {
                _db.Insert(admit);
                return RedirectToAction("Index");
            }
            return View(admit);
        }

        public ActionResult Edit(int id)
        {
            Admit admit = _db.GetbyId(id);
            ViewBag.DepartmentID = new SelectList(_dbDep.GetAll(), "DepartmentID", "DepartmentName");
            ViewBag.PatientID = new SelectList(_dbPat.GetAll(), "PatientID", "PatientName");
            return View(admit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(Admit admit)
        {
            if (ModelState.IsValid)
            {
                _db.Update(admit);
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentID = new SelectList(_dbDep.GetAll(), "DepartmentID", "DepartmentName");
            ViewBag.PatientID = new SelectList(_dbPat.GetAll(), "PatientID", "PatientName");
            return View(admit);
        }

        public ActionResult Delete(int id)
        {
            Admit admit = _db.GetbyId(id);
            return View(admit);
        }

        [HttpPost, ActionName("Delete")]

        public ActionResult DeleteConfirmed(int id)
        {
            _db.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            Admit admit = _db.GetbyId(id);
            return View(admit);
        }
    }
}