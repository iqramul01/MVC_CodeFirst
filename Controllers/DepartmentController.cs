using _1263087.Models;
using _1263087.Models.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _1263087.Controllers
{
    public class DepartmentController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Department
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "name_desec" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = currentFilter;

            var departments = from e in db.Departments
                              select e;

            if (!String.IsNullOrEmpty(searchString))
            {
                departments = departments.Where(e => e.DepartmentName.ToLower().Contains(searchString.ToLower()));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    {
                        departments = departments.OrderByDescending(e => e.DepartmentName);
                        break;
                    }
                default:
                    {
                        departments = departments.OrderBy(e => e.DepartmentName);
                        break;
                    }
            }

            switch (sortOrder)
            {
                case "name_desc":
                    {
                        departments = departments.OrderByDescending(e => e.DepartmentName);
                        break;
                    }

                default:
                    {
                        departments = departments.OrderBy(e => e.DepartmentName);
                        break;
                    }

            }
            int pageSize = 4;
            int pageNumber = (page ?? 1);

            return View(departments.ToPagedList(pageNumber, pageSize));
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DepartmentVM departmentVM)
        {
            Department department = new Department();
            if (ModelState.IsValid)
            {
                department.DepartmentName = departmentVM.DepartmentName;
                department.AvailableSeat = departmentVM.AvailableSeat;
                department.IsActive = departmentVM.IsActive;

                db.Departments.Add(department);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");

        }
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            Department department = db.Departments.SingleOrDefault(d => d.DepartmentID == id);
            var departmentVM = new DepartmentVM();
            departmentVM.DepartmentName = department.DepartmentName;
            departmentVM.AvailableSeat = department.AvailableSeat;
            departmentVM.IsActive = department.IsActive;
            return View(departmentVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DepartmentVM departmentVM, int id)
        {
            Department department = db.Departments.Find(id);
            if (ModelState.IsValid)
            {
                department.DepartmentName = departmentVM.DepartmentName;
                department.AvailableSeat = departmentVM.AvailableSeat;
                department.IsActive = departmentVM.IsActive;

                db.Entry(department).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return RedirectToAction("Index");

        }
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            Department department = db.Departments.SingleOrDefault(d => d.DepartmentID == id);
            var departmentVM = new DepartmentVM();
            departmentVM.DepartmentName = department.DepartmentName;
            departmentVM.AvailableSeat = department.AvailableSeat;
            departmentVM.IsActive = department.IsActive;
            return View(departmentVM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmedDelete(int id)
        {
            Department department = db.Departments.Find(id);
            if (department != null)
            {
                db.Departments.Remove(department);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        public JsonResult DoctorInfo(int id)
        {
            var docInfo = db.Doctors.Where(o => o.DoctorID == id).AsEnumerable().Select(o => new
            {
                id = o.DoctorID,
                docName = o.DoctorName,
                docEmail = o.EmailAddress,
                joiningDate = o.JoiningDate.ToString("yyyy-MM-dd"),
                docPhone = o.PhoneNumber.ToString(),
                docTotalBill = o.TotalBill.ToString()

            });
            return Json(docInfo, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(int id = 0)
        {
            Department department = db.Departments.Find(id);
            if (ModelState == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }
    }
}