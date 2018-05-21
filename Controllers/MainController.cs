using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using HallManagementSystem.Models;

namespace HallManagementSystem.Controllers
{
    public class MainController : Controller
    {
        //
        // GET: /Main/
        sb2 db = new sb2();
        public ActionResult Registration()
        {
            return View();

        }
        [HttpPost]
        public ActionResult Registration(string name, string faculty,string desig, string stuid, string reg,string session,string district,string contact,string blood,string hall,string email,string pwd, HttpPostedFileBase file)
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

            Registration regg = new Registration();
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
         


            db.Registrations.Add(regg);
            db.SaveChanges();

            return RedirectToAction("Login", "Main");

            //   return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string stuid, string pwd)
        {


            var usr = db.Registrations.SingleOrDefault(u => u.stuid == stuid && u.pwd == pwd);
            if (usr != null)
            {
                Session["userName"] = usr.name.ToString();
                Session["userId"] = usr.stuid.ToString();
                Session["userDesig"] = usr.desig.ToString();
                return RedirectToAction("AllPosts", "Main");
            }
            
            return View();


        }
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult University()
        {
            return View();
        }
        public ActionResult Gallery()
        {
            return View();
        }
        public ActionResult LoginSuccessful()
        {
            return View();
        }

        public ActionResult LoginSuccess()
        {
            return View();
        }
        public ActionResult LoginAdmission()
        {
            return View();
        }
        public ActionResult LoginHomeStudent()
        {
            return View();
        }
        public ActionResult LoginNotice()
        {
            return View();
        }
        public ActionResult StudentPost()
        {
            return View();
        }
        [HttpPost]
        public ActionResult StudentPost(string provostmessage, string title, string dateTime)
        {



            ProvostPost pst = new ProvostPost();

            pst.provostmessage = provostmessage;
            pst.title = title;
           
            pst.dateTime = DateTime.Now.ToString();
            pst.name = Session["userName"].ToString();
            pst.desig = Session["userDesig"].ToString();

            db.ProvostPosts.Add(pst);
            db.SaveChanges();
            return RedirectToAction("AllPosts", "Main");
        }

        public ActionResult StudentProvostMessage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult StudentProvostMessage(string secretmsg, string dateTime)
        {

            StudentProvostSecretMsg secret = new StudentProvostSecretMsg();

            secret.secretmsg= secretmsg;

            secret.dateTime = DateTime.Now.ToString();

            db.StudentProvostSecretMsgs.Add(secret);
            db.SaveChanges();
            return RedirectToAction("AllPosts", "Main");
        }

        public ActionResult StudentProvostContact()
        {
            return View();
        }
        public ActionResult AllPosts()
        {
            var post = (from s in db.ProvostPosts select s).AsEnumerable().Reverse();
            return View(post);
        }
        public ActionResult StudentsProfile(string id)
        {
            var info = from s in db.Registrations select s;
            if (info != null)
            {
                info = info.Where(c => c.stuid == id);
            }

            return View(info);
            //return Content(id.ToString());
         
        }
        public ActionResult StudentInsecureMessage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult StudentInsecureMessage(string msg ,string title,string dateTime)
        {
            StudentInsecureMessage message = new StudentInsecureMessage();

            message.msg = msg;
            message.title = title;
            message.dateTime = DateTime.Now.ToString();
            message.name = Session["userName"].ToString();
            message.desig = Session["userDesig"].ToString();

            db.StudentInsecureMessages.Add(message);
            db.SaveChanges();
            return RedirectToAction("AllPosts", "Main");
        }
        public ActionResult HallAdmission()
        {
            return View();
        }
        [HttpPost]
        public ActionResult HallAdmission(string name, string stuid, string regg, string session, string faculty, string semester, string hall, string room, string amount)
        {
 
            HallAdmission admission = new HallAdmission();
            admission.name = name;
            admission.faculty = faculty;
            admission.stuid = stuid;
            admission.regg = regg;
            admission.session = session;
            admission.semester = semester;
            admission.hall = hall;
            admission.room = room;
            admission.amount = amount;
            db.HallAdmissions.Add(admission);
            db.SaveChanges();

            return RedirectToAction("Login", "Main");

        }

        public ActionResult HallFees()
        {
            var item = from s in db.HallAdmissions select s;


            return View(item);
        }

	}
}