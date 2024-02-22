using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentWheels.Models;

namespace RentWheels.Controllers
{
    public class RentifyController : Controller
    {
        RentWheelsEntities db = new RentWheelsEntities();
        // GET: Rentify
        public ActionResult Rentify()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetCarNo()
        {
            var carNumbers = db.CarDetails.Select(c => new { CarNo = c.CarNo }).ToList();
            return Json(carNumbers, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetFees(string carNo)
        {
            var fees = db.CarDetails.Where(c => c.CarNo == carNo).Select(c => c.Fees).FirstOrDefault();
            return Json(fees, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveData(CarRentalViewModel model)
        {
            if (ModelState.IsValid)
            {
                var carRental = new CarRental
                {
                    CustomerName = model.CustomerName,
                    PhoneNo = model.PhoneNo,
                    CarNo = model.CarNo,
                    Fees = model.Fees,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    TotalAmount = model.TotalAmount
                };

                // Assuming db is your DbContext instance
                db.CarRentals.Add(carRental);
                db.SaveChanges();

                TempData["SuccessMessage"] = "Data successfully saved.";

                return RedirectToAction("Rentify", "Rentify");
            }

            return View("Rentify", model);
        }
    }
}