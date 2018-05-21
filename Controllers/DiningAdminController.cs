using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using HallManagementSystem.Models;
namespace HallManagementSystem.Controllers
{
    public class DiningAdminController : Controller
    {
        //
        // GET: /DiningAdmin/
        sb2 db = new sb2();

        public ActionResult Dining()
        {
            return View();
        }
        public ActionResult DiningRegistration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DiningRegistration(string name, string desig, string home, string contact, string hall, string email, string pwd, HttpPostedFileBase file)
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

            DiningRegistration dngregg = new DiningRegistration();
            dngregg.name = name;
            dngregg.desig = desig;
            dngregg.home = home;
            dngregg.contact = contact;
            dngregg.hall = hall;
            dngregg.email = email;
            dngregg.pwd = pwd;
            dngregg.image = photoPath;


            db.DiningRegistrations.Add(dngregg);
            db.SaveChanges();

            return RedirectToAction("Login", "Provost");

            //   return View();
        }

        public ActionResult DiningMenu()
        {
            return View();
        }
        public ActionResult DiningLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DiningMenu(int serial,string name,string price,  HttpPostedFileBase file)
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

          

            DiningMenu menu = new DiningMenu();
            menu.serial = serial;
            menu.name = name;
            menu.price = price;
            menu.image = photoPath;

            db.DiningMenus.Add(menu);
            db.SaveChanges();

            return RedirectToAction("Login", "Provost");

            //   return View();
        }
        public ActionResult DiningMenuView(int id)
        {
            var menuview = from s in db.DiningMenus select s;
            if (menuview !=null)
            {
                menuview = menuview.Where(c => c.id == id);
            }
            return View(menuview);
        }
	}
}