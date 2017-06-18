using ComplaintManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComplaintManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ContactUs()
        {
            return View();
        }

        public ActionResult FAQ()
        {
            return View();
        }

        public ActionResult SignUp()
        {
            return View();
        }

        public ActionResult Successful()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(tblCustomer obj)
        {
            dbCMSEntities db = new dbCMSEntities();

            try
            {
                db.tblCustomers.Add(obj);
                db.SaveChanges();
                return RedirectToAction("Successful", "Home");
            }
            catch (Exception)
            {
                ModelState.AddModelError("Error", "Email or Account Number already registered");
                return View();
            }
        }

        public ActionResult Error()
        {
            return View();
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(tblCustomer obj)
        {
            string accno = obj.AccountNumber;
            string email = obj.Email;

            dbCMSEntities db = new dbCMSEntities();

            var customer = (from c in db.tblCustomers
                            where c.AccountNumber.Equals(accno) && c.Email.Equals(email)
                            select c).FirstOrDefault();

            if (customer == null)
            {
                ModelState.AddModelError("Error", "Invalid Email or Account Number");
                return View();
            }

            Session["ResetId"] = customer.CustomerId;
            return RedirectToAction("ResetPassword");

        }

        public ActionResult ResetPassword()
        {
            dbCMSEntities db = new dbCMSEntities();

            try
            {
                string sessionid = Session["ResetId"].ToString();
                var customer = (from c in db.tblCustomers
                                where c.CustomerId.ToString().Equals(sessionid)
                                select c).First();

                customer.SecurityAnswer = null;
                customer.Pssd = null;

                return View(customer);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public ActionResult ResetPassword(tblCustomer obj)
        {
            dbCMSEntities db = new dbCMSEntities();
            try
            {
                string sessionid = Session["ResetId"].ToString();
                string answer = obj.SecurityAnswer;


                var customer = (from c in db.tblCustomers
                                where c.CustomerId.ToString().Equals(sessionid) && c.SecurityAnswer.Equals(answer)
                                select c).FirstOrDefault();

                if (customer == null)
                {
                    ModelState.AddModelError("Error", "Incorrect security answer");

                    var customernew = (from a in db.tblCustomers
                                       where a.CustomerId.ToString().Equals(sessionid)
                                       select a).First();
                    customernew.SecurityAnswer = null;
                    customernew.Pssd = null;

                    return View(customernew);
                }

                customer.ConPssd = obj.ConPssd;
                customer.Pssd = obj.Pssd;
                db.SaveChanges();

                return RedirectToAction("SuccessfulReset", "Login");
            }

            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }

        }

        public ActionResult SuccessfulReset()
        {
            return View();
        }
    }
}