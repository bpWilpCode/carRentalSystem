using Car_Rental_System.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Car_Rental_System.Controllers
{
    public class RentController : Controller
    {
        CarDBBitsEntities db = new CarDBBitsEntities();

        // GET: Rent
        public ActionResult Index()
        {
            var result = (from r in db.Rentals
                          join c in db.CarRegs on r.carid equals c.carno
                          select new RentalViewModel
                          {
                              id = r.id,
                              carid = r.carid,
                              custid = r.custid,
                              fee = r.fee,
                              sdate = r.sdate,
                              edate = r.edate,
                              available = c.available
                          }).ToList();
            return View(result);
        }
        [HttpGet]
        public ActionResult GetCar()
        {
            var car = db.CarRegs.ToList();
             return Json(car,JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Getid(int id)
        {
            var customer = (from s in db.Customers where s.id == id select s.custname).ToList();
            return Json(customer, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Getavail(String carno)
        {
            var caravail = (from s in db.CarRegs where s.carno == carno select s.available).FirstOrDefault();
            return Json(caravail, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult Save(Rental rent)
        {
            if (ModelState.IsValid)
            {
                db.Rentals.Add(rent);
                var car = db.CarRegs.SingleOrDefault(e => e.carno == rent.carid);
                if(car == null)
                {
                    return HttpNotFound("Car No is Not Valid");
                   
                }
                car.available = "No";
                db.Entry(car).State = EntityState.Modified;
                db.SaveChanges();   
                return RedirectToAction("Index");   
            }
            return View(rent);
        }


    }
}