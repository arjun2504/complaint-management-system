using ComplaintManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComplaintManagementSystem.Controllers
{
    public class SystemAdminController : Controller
    {
        public ActionResult Home()
        {
            try
            {
                if (Session["LoginType"].Equals("SystemAdmin") && Session["Id"] != null)
                {
                    dbCMSEntities db = new dbCMSEntities();
                    string sessionid = Session["Id"].ToString();
                    var systemadmin = (from sysa in db.tblSystemAdmins
                                       where sysa.SystemAdminId.ToString().Equals(sessionid)
                                    select sysa).FirstOrDefault();

                    var compdata = from c in db.tblComplaints
                                   select c;

                    var worker = from w in db.tblWorkers
                                 select w;

                    var prodadmin = from p in db.tblProductAdmins
                                    select p;

                    var customer = from c in db.tblCustomers
                                   select c;

                    SystemAdminView sav = new SystemAdminView() { complaintdata = compdata, workerdata = worker, productadmindata = prodadmin, systemadmindata = systemadmin, customerdata = customer };

                    return View(sav);
                }
                else
                    return RedirectToAction("Login", "Login");
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }
        public ActionResult Delete(string complaintid)
        {

            try
            {
                if (Session["LoginType"].Equals("SystemAdmin") && Session["Id"] != null)
                {
                    dbCMSEntities db = new dbCMSEntities();
                    var complainttable = (from i in db.tblComplaints
                                          where i.ComplaintId.ToString().Equals(complaintid) && i.ComplaintStatus.Equals("Closed")
                                          select i).FirstOrDefault();
                    db.tblComplaints.Remove(complainttable);
                    db.SaveChanges();
                    return RedirectToAction("Home", "SystemAdmin");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
            return RedirectToAction("Error", "Home");
        }

    }
}