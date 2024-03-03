using System.Linq;
using System.Web.Mvc;
using RentWheels.Models;

namespace RentWheels.Controllers
{
    [Authorize]
    [RoutePrefix("Rentify")]
    public class RentifyController : Controller
    {
        private RentWheelsEntities db = new RentWheelsEntities();

        [Route("")]
        public ActionResult Rentify()
        {
            return View();
        }

        [HttpGet]
        [Route("RentifyWithParams/{carNo}/{fees}")]
        public ActionResult RentifyWithParams(string carNo, int fees)
        {
            var model = new CarRentalViewModel
            {
                CarNo = carNo,
                Fees = fees
            };

            return View("Rentify", model);
        }

        [HttpGet]
        [Route("GetCarNo")]
        public ActionResult GetCarNo()
        {
            var carNumbers = db.CarDetails.Select(c => c.CarNo).ToList();
            return Json(carNumbers, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("GetFees/{carNo}")]
        public ActionResult GetFees(string carNo)
        {
            var fees = db.CarDetails.Where(c => c.CarNo == carNo).Select(c => c.Fees).FirstOrDefault();
            return Json(fees, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("GetAvil")]
        public ActionResult GetAvil(string carNo)
        {
            var carAvail = db.CarDetails.Where(c => c.CarNo == carNo).Select(c => c.Available).FirstOrDefault();
            return Json(carAvail, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("SaveData")]
        public ActionResult SaveData(CarRentalViewModel model)
        {
            if (ModelState.IsValid)
            {
                var carDetail = db.CarDetails.FirstOrDefault(c => c.CarNo == model.CarNo);
                if (carDetail != null)
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

                    if (db.RegisterDetails.Any(c => c.PhoneNo == carRental.PhoneNo))
                    {
                        db.CarRentals.Add(carRental);
                        db.SaveChanges();
                        //ViewBag.Message = "Car booked successfully.";
                        carDetail.Available = "No";
                        db.SaveChanges();
                    }
                    else
                    {
                       ViewBag.Message = "";
                    }
                }
                else
                {
                    ViewBag.Message = "Car not found.";
                }
                return RedirectToAction("CarList", "CarList");
            }
            return View("CarList", "CarList");
        }
    }
}
