using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentWheels.Models;

namespace RentWheels.Controllers
{
    [Authorize]
    public class CarBackController : Controller
    {
        RentWheelsEntities db = new RentWheelsEntities();
        // GET: CarReturn
        public ActionResult CarRental()
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
        public ActionResult GetAvil(string CarNo)
        {

            var CarAvil = (from s in db.CarDetails where s.CarNo == CarNo select s.Available).FirstOrDefault();
            return Json(CarAvil, JsonRequestBehavior.AllowGet);

        }

        // Action method to get the latest booking end date for a given car number
        public ActionResult GetLatestBookingEndDate(string carNo)
        {
            var latestEndDate = db.CarRentals
                .Where(c => c.CarNo == carNo)
                .OrderByDescending(c => c.EndDate)
                .Select(c => c.EndDate)
                .FirstOrDefault();

            return Json(latestEndDate, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveReturnData(CarBackViewModel model)
        {
            if (ModelState.IsValid)
            {
                var carReturn = new CarReturn
                {
                    CarNo = model.CarNo,
                    ReturnDate = model.ReturnDate,
                    ExpiredDays = model.ExpiredDays,
                    TotalAmount = model.TotalAmount
                };

                db.CarReturns.Add(carReturn);
               var num =  db.SaveChanges();
                if (num == 0)
                {

                }

                var carDetail = db.CarDetails.FirstOrDefault(c => c.CarNo == model.CarNo);
                if (carDetail != null)
                {
                    carDetail.Available = "Yes";
                    db.SaveChanges();
                }
                else
                {
                    TempData["ErrorMessage"] = "Car not found.";
                    return RedirectToAction("Rentify", "Rentify");
                }

                TempData["SuccessMessage"] = "Data successfully saved.";
                return RedirectToAction("CarRental", "CarBack");
            }

            // If model state is not valid, return back to the view with validation errors
            return View("CarBack", model);
        }


    }
}