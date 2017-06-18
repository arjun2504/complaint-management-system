using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComplaintManagementSystem.Models;

namespace ComplaintManagementSystem.Controllers
{
    public class CustomerController : Controller
    {
        public ActionResult Home()
        {
            try
            {
                if (Session["LoginType"].Equals("Customer") && Session["Id"] != null)
                {
                    dbCMSEntities db = new dbCMSEntities();
                    string sessionid = Session["Id"].ToString();
                    var customer = (from cus in db.tblCustomers
                                    where cus.CustomerId.ToString().Equals(sessionid)
                                    select cus).FirstOrDefault();

                    var complaint = (from com in db.tblComplaints
                                    where com.CustomerId.ToString().Equals(sessionid)
                                    select com);

                    CustomerView custview = new CustomerView() { Customerdata = customer, Complaintdata = complaint };

                    return View(custview);
                }
                else
                    return RedirectToAction("Login", "Login");
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult NewComplaint()
        {
            try
            {
                if (Session["Id"] == null)
                    return RedirectToAction("Login", "Login");
                else
                    return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        [ValidateInput(true)]
        public ActionResult NewComplaint(tblComplaint compobj)
        {
            try
            {
                if (Session["LoginType"].Equals("Customer") && Session["Id"] != null)
                {
                    dbCMSEntities db = new dbCMSEntities();
                    compobj.CustomerId = int.Parse(Session["Id"].ToString());
                    compobj.ComplaintStatus = "Not Assigned";
                    compobj.WorkerQuery = "NA";
                    compobj.CustomerReply = "NA";
                    compobj.Feedback = "NA";
                    compobj.ComplaintDate = DateTime.Now;
                    db.tblComplaints.Add(compobj);
                    db.SaveChanges();
                    return RedirectToAction("Home", "Customer");
                }
                else
                    return RedirectToAction("Login", "Login");
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }


        public ActionResult Reply(string complaintid)
        {
            dbCMSEntities db = new dbCMSEntities();

            try
            {
                if (Session["LoginType"].Equals("Customer") && Session["Id"] != null)
                {

                    string cid = complaintid;
                    string customerid = Session["Id"].ToString();

                    var comobj = (from c in db.tblComplaints
                                  where c.ComplaintId.ToString().Equals(cid) && c.CustomerId.ToString().Equals(customerid)
                                  select c).FirstOrDefault();

                    if (comobj.CustomerReply == "Awaiting Reply")
                    {
                        return View(comobj);
                    }
                    else
                    {
                        return RedirectToAction("Login", "Login");
                    }

                }
                else
                    return RedirectToAction("Login", "Login");
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public ActionResult Reply(tblComplaint comobj)
        {
            try
            {
                if (Session["LoginType"].Equals("Customer") && Session["Id"] != null)
                {
                    dbCMSEntities db = new dbCMSEntities();

                    var obj = (from c in db.tblComplaints
                               where c.ComplaintId.Equals(comobj.ComplaintId)
                               select c).FirstOrDefault();

                    obj.CustomerReply = comobj.CustomerReply;
                    db.SaveChanges();
                    return RedirectToAction("Home", "Customer");
                }
                else
                    return RedirectToAction("Login", "Login");
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult Feedback(string complaintid)
        {
            dbCMSEntities db = new dbCMSEntities();

            try
            {
                if (Session["LoginType"].Equals("Customer") && Session["Id"] != null)
                {
                    string compid = complaintid;
                    string customerid = Session["Id"].ToString();

                    var comobj = (from c in db.tblComplaints
                                  where c.ComplaintId.ToString().Equals(compid) && c.CustomerId.ToString().Equals(customerid)
                                  select c).FirstOrDefault();

                    if (comobj.ComplaintStatus == "Resolved")
                    {
                        return View(comobj);
                    }
                    else
                    {
                        return RedirectToAction("Login", "Login");
                    }
                }
                else
                    return RedirectToAction("Login", "Login");
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public ActionResult Feedback(tblComplaint comobj)
        {
            dbCMSEntities db = new dbCMSEntities();

            try
            {
                if (Session["LoginType"].Equals("Customer") && Session["Id"] != null)
                {
                    var obj = (from c in db.tblComplaints
                               where c.ComplaintId.Equals(comobj.ComplaintId)
                               select c).FirstOrDefault();

                    obj.ComplaintStatus = "Feedback Provided";
                    obj.Feedback = comobj.Feedback;
                    db.SaveChanges();
                    return RedirectToAction("Home", "Customer");
                }
                else
                    return RedirectToAction("Login", "Login");
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }
    }
}