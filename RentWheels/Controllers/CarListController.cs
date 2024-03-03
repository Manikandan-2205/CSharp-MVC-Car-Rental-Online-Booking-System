using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentWheels.Models;

namespace RentWheels.Controllers
{
    public class CarListController : Controller
    {
        RentWheelsEntities db = new RentWheelsEntities();
        // GET: CarList
        public ActionResult CarList()
        {
            var CarList1 = (from listDetails in db.CarDetails
                            select new CarListDetailsViewModel
                            {
                                Id = listDetails.Id,
                                CarNo = listDetails.CarNo,
                                ModelName = listDetails.ModelName,
                                BrandName = listDetails.BrandName,
                                Color = listDetails.Color,
                                RideSelection = listDetails.RideSelection,
                                Fees = listDetails.Fees,
                                FuelType = listDetails.FuelType,
                                Travel = listDetails.Travel,
                                Available = listDetails.Available,
                                ImageUrl = listDetails.ImageUrl
                            }).ToList();
            //var discountList = (from discount in db.DisCounts
            //                    select discount).ToList();

            //ViewBag.Discounts = discountList;
            return View(CarList1);
        }

        // Action to handle filtering
        public ActionResult Filter(string rideSelection, string color, int? Price)
        {
            var filteredCars = db.CarDetails.AsQueryable();

            if (!string.IsNullOrEmpty(rideSelection))
            {
                filteredCars = filteredCars.Where(car => car.RideSelection == rideSelection);
            }

            if (!string.IsNullOrEmpty(color))
            {
                filteredCars = filteredCars.Where(car => car.Color == color);
            }

            if (!string.IsNullOrEmpty(Price.ToString()))
            {
                
                var a = Convert.ToInt32(Price);                
               filteredCars = filteredCars.Where(car => car.Fees == a);
                
            }

            var filteredCarList = filteredCars.Select(car => new CarListDetailsViewModel
            {
                Id = car.Id,
                CarNo = car.CarNo,
                ModelName = car.ModelName,
                BrandName = car.BrandName,
                Color = car.Color,
                RideSelection = car.RideSelection,
                Fees = car.Fees,
                FuelType = car.FuelType,
                Travel = car.Travel,
                Available = car.Available,
                ImageUrl = car.ImageUrl
            }).ToList();

            return View("CarList", filteredCarList);
        }

    }
}