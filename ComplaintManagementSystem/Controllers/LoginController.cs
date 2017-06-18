using ComplaintManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComplaintManagementSystem.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            Session.RemoveAll();
            return View();
        }

        [HttpPost]
        [ValidateInput(true)]
        public ActionResult Login(Login credentials)
        {
            string Email = credentials.Email;
            string Pssd = credentials.Pssd;
            string LoginType = credentials.LoginType;

            dbCMSEntities dbo = new dbCMSEntities();

            try
            {
                if (LoginType == "SystemAdmin")
                {
                    var user = (from u in dbo.tblSystemAdmins
                                where u.Email.Equals(Email) && u.Pssd.Equals(Pssd)
                                select u).FirstOrDefault();


                    if (user.Pssd == Pssd)
                    {
                        Session["Id"] = user.SystemAdminId;
                        Session["LoginType"] = LoginType;
                        return RedirectToAction("Home", "SystemAdmin");
                    }

                    ModelState.AddModelError("Error", "Invalid Email or Password");
                    return View();
                }

                else if (LoginType == "ProductAdmin")
                {
                    var user = (from u in dbo.tblProductAdmins
                                where u.Email.Equals(Email) && u.Pssd.Equals(Pssd)
                                select u).FirstOrDefault();

                    if (user.Pssd == Pssd)
                    {
                        Session["Id"] = user.ProductAdminId;
                        Session["LoginType"] = LoginType;
                        return RedirectToAction("Home", "ProductAdmin");
                    }

                    ModelState.AddModelError("Error", "Invalid Email or Password");
                    return View();
                }

                else if (LoginType == "Worker")
                {
                    var user = (from u in dbo.tblWorkers
                                where u.Email.Equals(Email) && u.Pssd.Equals(Pssd)
                                select u).FirstOrDefault();

                    if (user.Pssd == Pssd)
                    {
                        Session["Id"] = user.WorkerId;
                        Session["LoginType"] = LoginType;
                        return RedirectToAction("Home", "Worker");
                    }

                    ModelState.AddModelError("Error", "Invalid Email or Password");
                    return View();
                }

                else if (LoginType == "Customer")
                {
                    var user = (from u in dbo.tblCustomers
                                where u.Email.Equals(Email) && u.Pssd.Equals(Pssd)
                                select u).FirstOrDefault();

                    if (user.Pssd == Pssd)
                    {
                        Session["Id"] = user.CustomerId;
                        Session["LoginType"] = LoginType;
                        return RedirectToAction("Home", "Customer");
                    }

                    ModelState.AddModelError("Error", "Invalid Email or Password");
                    return View();
                }
            }

            catch (System.NullReferenceException)
            {
                ModelState.AddModelError("Error", "Invalid Email or Password");
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(Login credentials)
        {
            dbCMSEntities dbo = new dbCMSEntities();

            string Pssd = credentials.Pssd;
            string LoginType = Session["LoginType"].ToString();
            string NewPssd = credentials.NewPssd;
            int uid = int.Parse(Session["Id"].ToString());


            if (LoginType == "SystemAdmin")
            {
                var user = (from u in dbo.tblSystemAdmins
                            where u.SystemAdminId.Equals(uid) && u.Pssd.Equals(Pssd)
                            select u).FirstOrDefault();

                try
                {
                    if (user.Pssd == Pssd)
                    {
                        if(Pssd==NewPssd)
                        {
                            ModelState.AddModelError("Error", "Old and New Password cannot be the same");
                            return View();
                        }
                        user.Pssd = NewPssd;
                        dbo.SaveChanges();
                        return RedirectToAction("Login", "Login");
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("Error", "Invalid Old Password");
                    return View();
                }
            }

            else if (LoginType == "ProductAdmin")
            {
                var user = (from u in dbo.tblProductAdmins
                            where u.ProductAdminId.Equals(uid) && u.Pssd.Equals(Pssd)
                            select u).FirstOrDefault();

                try
                {
                    if (user.Pssd == Pssd)
                    {
                        if (Pssd == NewPssd)
                        {
                            ModelState.AddModelError("Error", "Old and New Password cannot be the same");
                            return View();
                        }
                        user.Pssd = NewPssd;
                        dbo.SaveChanges();
                        return RedirectToAction("Login", "Login");
                    }
                }

                catch (Exception)
                {
                    ModelState.AddModelError("Error", "Invalid Old Password");
                    return View();
                }
            }

            else if (LoginType == "Worker")
            {
                var user = (from u in dbo.tblWorkers
                            where u.WorkerId.Equals(uid) && u.Pssd.Equals(Pssd)
                            select u).FirstOrDefault();
                try
                {

                    if (user.Pssd == Pssd)
                    {
                        if (Pssd == NewPssd)
                        {
                            ModelState.AddModelError("Error", "Old and New Password cannot be the same");
                            return View();
                        }
                        user.Pssd = NewPssd;
                        dbo.SaveChanges();
                        return RedirectToAction("Login", "Login");
                    }
                }

                catch (Exception)
                {
                    ModelState.AddModelError("Error", "Invalid Old Password");
                    return View();
                }
            }

            else if (LoginType == "Customer")
            {
                var user = (from u in dbo.tblCustomers
                            where u.CustomerId.Equals(uid) && u.Pssd.Equals(Pssd)
                            select u).FirstOrDefault();

                try
                {
                    if (user.Pssd == Pssd)
                    {
                        if (Pssd == NewPssd)
                        {
                            ModelState.AddModelError("Error", "Old and New Password cannot be the same");
                            return View();
                        }
                        user.Pssd = NewPssd;
                        user.ConPssd = NewPssd;
                        dbo.SaveChanges();
                        return RedirectToAction("Login", "Login");
                    }
                }

                catch (Exception)
                {
                    ModelState.AddModelError("Error", "Invalid Old Password");
                    return View();
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}