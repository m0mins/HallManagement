using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HallManagementSystem.Models;
using System.IO;
using System.Data.Entity;

namespace HallManagementSystem.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        sb2 db = new sb2();

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string email, string pwd)
        {


            if (email=="admin@gmail.com" && pwd=="123")
            {               
                return RedirectToAction("AllPosts", "Admin");
            }
            return View();


        }
        public ActionResult AllStudents()
        {
            return View(db.Registrations.ToList());
        }
        public ActionResult AllPosts()
        {
            var post = (from s in db.ProvostPosts select s).AsEnumerable().Reverse();
            return View(post);
        }
        public ActionResult studentDetail(string id)
        {
            var student = from s in db.Registrations select s;
            if (student != null)
            {
                student = student.Where(c => c.stuid == id);
            }

            return View(student);
        }
        


        public ActionResult removePermission(string id)
        {
            Session["removeId"] = id;
            return View();
        }

        public ActionResult removePermission2(string id)
        {
            if (id == "yes")
            {
                string studentId = Session["removeID"].ToString();
                Registration rs = db.Registrations.Find(studentId);
                db.Registrations.Remove(rs);
                db.SaveChanges();
                return RedirectToAction("AllStudents");
            }

            else if (id == "no")
            {
                return RedirectToAction("AllPosts");
            }
            else
            {
                return View();
            }

        }

      
        public ActionResult Edit(string id)
        {
            var student = from s in db.Registrations select s;
            if (student != null)
            {
                student = student.Where(c => c.stuid == id);
            }

            return View(student);

        }


        //

        [HttpPost]
        public ActionResult Registration(string name, string faculty, string desig, string stuid, string reg, string session, string district, string contact, string blood, string hall, string email, string pwd, HttpPostedFileBase file)
        {

            var path = "";
            var photoPath = "";

            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    if (Path.GetExtension(file.FileName).ToLower() == ".jpg"
                        || Path.GetExtension(file.FileName).ToLower() == ".png"
                              || Path.GetExtension(file.FileName).ToLower() == ".gif"
                                    || Path.GetExtension(file.FileName).ToLower() == ".jpeg")
                    {
                        path = Path.Combine(Server.MapPath("~/Content"), file.FileName);
                        file.SaveAs(path);
                        //photoPath = "~/Images/" + file.FileName; 
                        photoPath = file.FileName;
                    }
                }
            }

           

           // Registration regg = new Registration();


            var regg = (from s in db.Registrations where s.stuid == stuid select s).FirstOrDefault();
            regg.name = name;
            regg.faculty = faculty;
            regg.desig = desig;
            regg.stuid = stuid;
            regg.reg = reg;
            regg.session = session;
            regg.district = district;
            regg.contact = contact;
            regg.blood = blood;
            regg.hall = hall;
            regg.email = email;
            regg.pwd = pwd;
            regg.image = photoPath;
            db.Entry(regg).State = EntityState.Modified;
            db.SaveChanges();

            return Content("doneee");

            //db.Registrations.Add(regg);
            //db.SaveChanges();

           // return RedirectToAction("Login", "Main");

            //   return View();
        }

        //

        //[HttpPost]
        //public ActionResult Edit(Registration ri)
        //{
        //    db.Entry(ri).State = EntityState.Modified;
        //    db.SaveChanges();
        //    return RedirectToAction("AllStudents");
        //}
       

        public ActionResult notify()
        {
            return View();
        }
        public ActionResult ProvostSpeech()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ProvostSpeech(string name, string desig,string faculty, string speech, HttpPostedFileBase file)
        {
            var path = "";
            var photoPath = "";

            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    if (Path.GetExtension(file.FileName).ToLower() == ".jpg"
                        || Path.GetExtension(file.FileName).ToLower() == ".png"
                              || Path.GetExtension(file.FileName).ToLower() == ".gif"
                                    || Path.GetExtension(file.FileName).ToLower() == ".jpeg")
                    {
                        path = Path.Combine(Server.MapPath("~/Content"), file.FileName);
                        file.SaveAs(path);
                        //photoPath = "~/Images/" + file.FileName; 
                        photoPath = file.FileName;
                    }
                }
            }
            ProvostSpeech spch = new ProvostSpeech();
            spch.name = name;
            spch.desig = desig;
            spch.faculty = faculty;
            spch.speech = speech;
            spch.image = photoPath;

            db.ProvostSpeeches.Add(spch);
            db.SaveChanges();
            
            return RedirectToAction("Login");
        }
        public ActionResult ProvostSpeechDetail()
        {
            var speech = from s in db.ProvostSpeeches select s;
            //if (speech != null)
            //{
            //    speech = speech.Where(c => c.id == id);
          //  }

            return View(speech);
        }
      
       
	}
}