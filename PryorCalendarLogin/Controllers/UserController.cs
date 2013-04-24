using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PryorCalendarLogin.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(PryorCalendarLogin.Models.User user)
        {
            if (ModelState.IsValid)
            {
                if(IsValid(user.User_Name, user.Password))
                {
                    FormsAuthentication.SetAuthCookie(user.User_Name, false);
                    return RedirectToAction("Index", "Home");
                }

                else
                {
                    ModelState.AddModelError("", "Login data is incorrect.");
                }
            }

            return View(user);
        }

        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(PryorCalendarLogin.Models.User user)
        {
            if (ModelState.IsValid)
            {
                using (var db = new PryorCalendarLogin.Models.CalendarDBEntities2())
                {
                    var newUser = db.Users.Create();

                    newUser.User_ID = Guid.NewGuid();
                    newUser.User_Name = user.User_Name;
                    newUser.Password = user.Password;
                    newUser.First_Name = user.First_Name;
                    newUser.Last_Name = user.Last_Name;
                    newUser.Email = user.Email;
                    newUser.Phone_Num = user.Phone_Num;

                    db.Users.Add(newUser);
                    db.SaveChanges();

                    return RedirectToAction("Index", "Home");
                }
            }

            return View(user);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        private bool IsValid(string UserName, string Password)
        {
            bool isValid = false;

            using (var db = new PryorCalendarLogin.Models.CalendarDBEntities2())
            {
                var user = db.Users.FirstOrDefault(u => u.User_Name == UserName);

                if (user != null)
                {
                    if (user.Password == Password)
                    {
                        isValid = true;
                    }
                }
            }

            return isValid;
        }
    }
}
