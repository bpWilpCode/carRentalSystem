using Car_Rental_System.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Car_Rental_System.Controllers
{
    public class ReturnCarController : Controller
    {
        CarDBBitsEntities db = new CarDBBitsEntities();
        // GET: ReturnCar
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Saveit(ReturnCar retcar)
        {
            if (ModelState.IsValid)
            {
                db.ReturnCars.Add(retcar);
                var car = db.CarRegs.SingleOrDefault(e => e.carno == retcar.carno);
                if(car == null)
                {
                    return HttpNotFound("CarNo Not Found");
                }
                car.available = "Yes";
                db.Entry(car).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");   
            }
            return View(retcar);
        }
        [HttpPost]
        public ActionResult Getid(String carno)
        {
            var carn = (from s in db.Rentals
                         where s.carid == carno
                         select new
                         {
                             StartDate = s.sdate,
                             EndDate = s.edate,
                             Custid = s.custid,
                             Fee = s.fee,
                             ElapsedDays = SqlFunctions.DateDiff("day", s.edate, DateTime.Now)
                         }).ToArray();
            return Json(carn, JsonRequestBehavior.AllowGet);
          
        }
    }
}