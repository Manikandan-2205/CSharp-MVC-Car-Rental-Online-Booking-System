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
            return View(CarList1);
        }
    }
}