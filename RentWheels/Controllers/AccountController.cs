using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentWheels.Models;
using RentWheels.ViewModels;
using System.Web.Security;

namespace RentWheels.Controllers
{
    public class AccountController : Controller
    {
        RentWheelsEntities db = new RentWheelsEntities();

        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password, string phoneNo)
        {
            if (ModelState.IsValid)
            {
                var user = db.RegisterDetails.FirstOrDefault(u => u.Username == username && u.Password == password && u.PhoneNo ==phoneNo);
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.Username, true);
                    TempData["Username"] = user.Username; 
                    TempData["PhoneNo"] = user.PhoneNo;
                    return RedirectToAction("CarList","CarList"); 
                }
                else
                {
                    ViewBag.Message = "Invalid username or password.";
                    return View();
                }
            }
            return View();
        }


        public ActionResult Register()
        {
            try
            {
                using (var db = new RentWheelsEntities())
                {
                    var viewModel = new RegisterViewModel
                    {
                        RegisterDetail = new RegisterDetail(),
                        RoleDetails = db.RoleDetails.ToList()
                    };

                    return View(viewModel);
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var existingEmail = db.RegisterDetails.FirstOrDefault(u => u.Email == viewModel.RegisterDetail.Email);
                var existingPhone = db.RegisterDetails.FirstOrDefault(u => u.PhoneNo == viewModel.RegisterDetail.PhoneNo);

                if (existingEmail != null)
                {
                    ViewBag.Message = "An account with this email already exists.";
                }
                else if (existingPhone != null)
                {
                    ViewBag.Message = "An account with this phone number already exists.";
                }
                else
                {
                    viewModel.RegisterDetail.RoleId = 2;
                    db.RegisterDetails.Add(viewModel.RegisterDetail);
                    db.SaveChanges();

                    ViewBag.Message = "Registration successful. Please log in.";

                    return RedirectToAction("Login", "Account");
                }
            }

            return View(viewModel);
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(string emailOrPhone)
        {
            return RedirectToAction("ResetPassword");

        }

        public ActionResult OTPProcessing(string emailOrPhone, string otp)
        {
            var user = db.RegisterDetails.FirstOrDefault(u => u.Email == emailOrPhone || u.PhoneNo == emailOrPhone);
            if (user != null)
            {
                if (string.IsNullOrEmpty(otp))
                {
                    Random random = new Random();
                    int otpNumber = random.Next(100000, 999999);
                    string generatedOtp = otpNumber.ToString();

                    Session["OTP"] = generatedOtp;
                    TempData["EmailOrPhone"] = emailOrPhone;

                    // Send OTP via email or SMS (you need to implement this part)

                    //ViewBag.Message = generatedOtp;                   

                    return Content("OTP sent successfull ! Your OTP is: " + generatedOtp);
                }
                else
                {
                    string storedOtp = Session["OTP"] as string;
                    if (otp == storedOtp)
                    {
                        return Content("OTP verification successfull.");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid OTP.");
                        return View();
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "User not found.");
                return View();
            }
        }

        public ActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ResetPassword(string newPassword, string confirmPassword)
        {
            string emailOrPhone = TempData["EmailOrPhone"].ToString();
            var user = db.RegisterDetails.FirstOrDefault(u => u.Email == emailOrPhone || u.PhoneNo == emailOrPhone);
            if (user != null)
            {
                if (newPassword != confirmPassword)
                {
                    ViewBag.Message = "The new password and confirm password do not match.";
                    return View();
                }

                user.Password = newPassword;
                db.SaveChanges();
                ViewBag.Message = "Password reset successful. You can now login with your new password.";
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ViewBag.Message = "User not found.";
                return View();
            }
        }

        [HttpGet]
        public ActionResult Signout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
