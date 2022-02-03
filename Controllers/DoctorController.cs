using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data.Entity;
using _1263087.Models;

namespace _1263087.Controllers
{
    public class DoctorController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Doctor
        public JsonResult IsEmailExists(string EmailAddress)
        {
            return Json(!db.Doctors.Any(d => d.EmailAddress == EmailAddress), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index()
        {
            return View(db.Doctors.ToList());
        }

        public ActionResult Create()
        {
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepartmentName");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Doctor dr)
        {
            if (ModelState.IsValid)
            {
                string fileName = Path.GetFileNameWithoutExtension(dr.UploadImage.FileName);
                string extension = Path.GetExtension(dr.UploadImage.FileName);
                HttpPostedFileBase postedFile = dr.UploadImage;
                int length = postedFile.ContentLength;
                if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                {
                    if (length <= 1000000)
                    {
                        fileName = fileName + extension;                      
                        dr.DoctorImage = "~/Images/" + fileName;
                        
                        fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                        dr.UploadImage.SaveAs(fileName);
                        db.Doctors.Add(dr);
                        int a = db.SaveChanges();
                        if (a > 0)
                        {
                            TempData["CreateMessage"] = "<script>alert('Record Inserted Successfully')</script>";
                            ModelState.Clear();
                            return RedirectToAction("Index", "Doctor");
                        }
                        else
                        {
                            TempData["CreateMessage"] = "<script>alert('Record Not Inserted ')</script>";
                        }

                    }
                    else
                    {
                        TempData["SizeMessage"] = "<script>alert('Image Size Should be less than 1 mb ')</script>";
                    }
                }
                else
                {
                    TempData["ExtensionMessage"] = "<script>alert('Image Format Not Supported ')</script>";
                }
            }
            return View();
        }

        
        public ActionResult Edit(int id)
        {
            var DoctorRow = db.Doctors.Where(t => t.DoctorID == id).FirstOrDefault();
            Session["Image"] = DoctorRow.DoctorImage;
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepartmentName");
            return View(DoctorRow);

        }

        [HttpPost]
        public ActionResult Edit(Doctor dr)
        {
            if (ModelState.IsValid)
            {
                if (dr.UploadImage != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(dr.UploadImage.FileName);
                    string extension = Path.GetExtension(dr.UploadImage.FileName);
                    HttpPostedFileBase postedFile = dr.UploadImage;
                    int length = postedFile.ContentLength;
                    if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                    {
                        if (length <= 1000000)
                        {
                            fileName = fileName + extension;
                            dr.DoctorImage = "~/Images/" + fileName;
                            fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                            dr.UploadImage.SaveAs(fileName);
                            db.Entry(dr).State = EntityState.Modified;
                            int a = db.SaveChanges();

                            if (a > 0)
                            {
                                //TempData["UpdateMessage"] = "<script>alert('Record Updated Successfully')</script>";
                                ModelState.Clear();
                                return RedirectToAction("Index", "Doctor");
                            }
                            else
                            {
                                TempData["UpdateMessage"] = "<script>alert('Record Not Updated ')</script>";
                            }
                        }
                        else
                        {
                            TempData["SizeMessage"] = "<script>alert('Image Size Should be less than 1 mb ')</script>";
                        }
                    }

                    else
                    {
                        TempData["ExtensionMessage"] = "<script>alert('Image Format Not Supported ')</script>";
                    }
                }
                else
                {
                    dr.DoctorImage = Session["Image"].ToString();
                    db.Entry(dr).State = EntityState.Modified;
                    int a = db.SaveChanges();

                    if (a > 0)
                    {
                        
                        ModelState.Clear();
                        return RedirectToAction("Index", "Doctor");
                    }
                    else
                    {
                        TempData["UpdateMessage"] = "<script>alert('Record Not Updated ')</script>";
                    }
                }
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepartmentName");
            return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id = 0)
        {
            Doctor doctor = db.Doctors.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id = 0)
        {
            Doctor doctor = db.Doctors.Find(id);
            db.Doctors.Remove(doctor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id = 0)
        {
            Doctor doctor = db.Doctors.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }
    }
}