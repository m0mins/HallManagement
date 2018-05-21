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
    public class ProvostController : Controller
    {
        //
        // GET: /Provost/
        sb2 db = new sb2();

        public ActionResult ProvostRegistration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ProvostRegistration(string name, string desig, string faculty, string home, int contact,  string hall, string email, string pwd, HttpPostedFileBase file)
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

            ProvostsRegistration proregg = new ProvostsRegistration();
            proregg.name = name;
            proregg.desig = desig;
            proregg.faculty = faculty;
            proregg.home = home;
            proregg.contact = contact;
            proregg.hall = hall;
            proregg.email = email;
            proregg.pwd = pwd;
            proregg.image = photoPath;


            db.ProvostsRegistrations.Add(proregg);
            db.SaveChanges();

            return RedirectToAction("Login", "Provost");

            //   return View();
        }

        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string email, string pwd)
        {


            var usr = db.ProvostsRegistrations.SingleOrDefault(u => u.email == email && u.pwd == pwd);
            if (usr != null)
            {
                Session["userName"] = usr.name.ToString();
                Session["userDesig"] = usr.desig.ToString();
                Session["userContact"] = usr.contact.ToString();
                return RedirectToAction("allPosts", "Provost");
            }
            return View();


        }
        public ActionResult AllStudents()
        {
            return View(db.Registrations.ToList());
        }
        
        public ActionResult LoginHomeProvost()
        {
            return View();
        }
        public ActionResult ProvostPost()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ProvostPost(string provostmessage,string time,string date,string desig,string title,string dateTime)
        {


            ProvostPost pst = new ProvostPost();

            pst.provostmessage = provostmessage;
            pst.title = title;
          //  pst.date = date;
            pst.dateTime = DateTime.Now.ToString();
            pst.name = Session["userName"].ToString();
            pst.desig = Session["userDesig"].ToString();
            db.ProvostPosts.Add(pst);
            db.SaveChanges();
            return RedirectToAction("allPosts", "Provost");
        }

        public ActionResult studentDetail(string id)
        {
            var student = from s in db.Registrations select s;
            if (student !=null)
            {
                student = student.Where(c => c.stuid == id);
            }

            return View(student);
        }

        
        public ActionResult AllPosts()
        {
            var post = (from s in db.ProvostPosts select s).AsEnumerable().Reverse();
            return View(post);
        }
        public ActionResult StudentSecretMessage()
        {
            return View(db.StudentProvostSecretMsgs.ToList());
        }
        public ActionResult AllSecretMessages(int id)
        {
            var messages = from s in db.StudentProvostSecretMsgs select s;
            if (messages != null)
            {
                messages = messages.Where(c => c.id == id);
            }

            return View(messages);
        }
        public ActionResult ProvostProfile(int id)
        {
            var info = from s in db.ProvostsRegistrations select s;
            if(info !=null)
            {
                info = info.Where(c => c.contact == id);
            }
            return View(info);
        }
        public ActionResult ReceiveInsecureMessage()
        {
            return View(db.StudentInsecureMessages.ToString());
        }
        public ActionResult StudentInsecureMessage(int id)
        {
            var messages = from s in db.StudentProvostSecretMsgs select s;
            if (messages != null)
            {
                messages = messages.Where(c => c.id == id);
            }

            return View(messages);
        }

        public ActionResult approveStudent()
        {
            var item = from s in db.HallAdmissions select s;
            return View(item);
           
        }

        //

        public ActionResult approveUpdate(string id)
        {
            if (ModelState.IsValid)
            {

                var model = (from p in db.HallAdmissions where p.stuid == id select p).FirstOrDefault();
                model.status = "Approved!!!";
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
               
               // return RedirectToAction("SemInfoList");
            }
            return Content("doneeeeeeeeee");
            
        }

       


        
	}
}