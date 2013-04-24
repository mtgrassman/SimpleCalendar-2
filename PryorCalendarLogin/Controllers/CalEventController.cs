using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PryorCalendarLogin.Models;

namespace PryorCalendarLogin.Controllers
{
    public class CalEventController : Controller
    {
        private CalendarDBEntities2 db = new CalendarDBEntities2();

        //
        // GET: /CalEvent/

        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                var cal_event = from c in db.Cal_Event
                                where c.User.User_Name == User.Identity.Name
                                select c;

                return View(cal_event.ToList());
            }
            else
            {
                return Redirect("User/LogIn");
            }
        }

        //
        // GET: /CalEvent/Details/5

        public ActionResult Details(int id = 0)
        {
            Cal_Event cal_event = db.Cal_Event.Find(id);
            if (cal_event == null)
            {
                return HttpNotFound();
            }
            return View(cal_event);
        }

        //
        // GET: /CalEvent/Create

        public ActionResult Create()
        {
            ViewBag.User_ID = new SelectList(db.Users, "User_ID", "User_Name");
            return View();
        }

        //
        // POST: /CalEvent/Create

        [HttpPost]
        public ActionResult Create(Cal_Event cal_event)
        {
            if (ModelState.IsValid)
            {
                db.Cal_Event.Add(cal_event);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.User_ID = new SelectList(db.Users, "User_ID", "User_Name", cal_event.User_ID);
            return View(cal_event);
        }

        //
        // GET: /CalEvent/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Cal_Event cal_event = db.Cal_Event.Find(id);
            if (cal_event == null)
            {
                return HttpNotFound();
            }
            ViewBag.User_ID = new SelectList(db.Users, "User_ID", "User_Name", cal_event.User_ID);
            return View(cal_event);
        }

        //
        // POST: /CalEvent/Edit/5

        [HttpPost]
        public ActionResult Edit(Cal_Event cal_event)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cal_event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.User_ID = new SelectList(db.Users, "User_ID", "User_Name", cal_event.User_ID);
            return View(cal_event);
        }

        //
        // GET: /CalEvent/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Cal_Event cal_event = db.Cal_Event.Find(id);
            if (cal_event == null)
            {
                return HttpNotFound();
            }
            return View(cal_event);
        }

        //
        // POST: /CalEvent/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Cal_Event cal_event = db.Cal_Event.Find(id);
            db.Cal_Event.Remove(cal_event);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}