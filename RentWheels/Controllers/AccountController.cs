using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentWheels.Models;
using RentWheels.ViewModels;

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
        public ActionResult Login(RegisterDetail model)
        {
            if (ModelState.IsValid)
            {
                var user = db.RegisterDetails.SingleOrDefault(u => u.PhoneNo == model.PhoneNo);

                if (user != null && user.Password == model.Password)
                {
                    // Successful login
                    TempData["Message"] = "Login successful.";
                }
                else
                {
                    // Unsuccessful login
                    TempData["Message"] = "Invalid PhoneNo or password.";
                }
            }
            else
            {
                // Model state is not valid
                TempData["Message"] = "Login failed. Please provide valid information.";
            }

            // Redirect to the login page
            return View(model);
        }


        public ActionResult Register()
        {
            var viewModel = new RegisterViewModel
            {
                RegisterDetail = new RegisterDetail(),
                RoleDetails = db.RoleDetails.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Check if the email or phone number already exists in the database
                var existingEmail = db.RegisterDetails.FirstOrDefault(u => u.Email == viewModel.RegisterDetail.Email);
                var existingPhone = db.RegisterDetails.FirstOrDefault(u => u.PhoneNo == viewModel.RegisterDetail.PhoneNo);

                if (existingEmail != null)
                {
                    ModelState.AddModelError("", "An account with this email already exists.");
                }
                else if (existingPhone != null)
                {
                    ModelState.AddModelError("", "An account with this phone number already exists.");
                }
                else
                {
                    // Set default role ID to 2 (User)
                    viewModel.RegisterDetail.RoleId = 2;

                    // Add the new user to the database
                    db.RegisterDetails.Add(viewModel.RegisterDetail);
                    db.SaveChanges();

                    // Set confirmation message
                    TempData["Message"] = "Registration successful. Please log in.";

                    // Redirect to the login page after successful registration
                    return RedirectToAction("Login", "Account");
                }
            }

            // If ModelState is not valid or if there are errors, reload the page with the ViewModel
            //viewModel.RegisterDetail = db.RegisterDetails.ToList();
            return View(viewModel);
        }


        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(string emailOrPhone)
        {
            // Validate the email or phone and send OTP
            // For demonstration purposes, let's assume the OTP is "1234"
            var user = db.RegisterDetails.FirstOrDefault(u => u.Email == emailOrPhone || u.PhoneNo == emailOrPhone);
            if (user != null)
            {
                // Generate OTP and send it to the provided email or phone number (Not implemented here)
                TempData["OTP"] = "1234"; // For demonstration purposes only
                TempData["EmailOrPhone"] = emailOrPhone;
                return RedirectToAction("VerifyOTP");
            }
            else
            {
                ModelState.AddModelError("", "User not found.");
                return View();
            }
        }

        // Verify OTP Action
        public ActionResult VerifyOTP()
        {
            // Generate a random 6-digit OTP
            Random random = new Random();
            int otpNumber = random.Next(100000, 999999);
            string otp = otpNumber.ToString();

            // Store the OTP in TempData for verification
            TempData["OTP"] = otp;

            // Display an alert message with the OTP
            ViewBag.OTPMessage = "Your OTP is: " + otp;

            return View();
        }

        [HttpPost]
        public ActionResult VerifyOTP(string otp)
        {
            if (otp == TempData["OTP"].ToString())
            {
                return RedirectToAction("ResetPassword");
            }
            else
            {
                ModelState.AddModelError("", "Invalid OTP.");
                return View();
            }
        }

        // Reset Password Action
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
                    ModelState.AddModelError("", "The new password and confirm password do not match.");
                    return View();
                }

                user.Password = newPassword; // Update the password
                db.SaveChanges();
                TempData["Message"] = "Password reset successful. You can now login with your new password.";
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ModelState.AddModelError("", "User not found.");
                return View();
            }
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
