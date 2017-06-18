using ComplaintManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComplaintManagementSystem.Controllers
{
    public class ProductAdminController : Controller
    {
        public ActionResult Home()
        {
            try
            {
                if (Session["LoginType"].Equals("ProductAdmin") && Session["Id"] != null)
            {
                dbCMSEntities db = new dbCMSEntities();
                string sessionid = Session["Id"].ToString();

                var productadmin = (from proda in db.tblProductAdmins
                                    where proda.ProductAdminId.ToString().Equals(sessionid)
                                    select proda).FirstOrDefault();

                string paid = productadmin.ProductAdminId.ToString();

                var productdata = (from p in db.tblProducts
                                   where p.ProductAdminId.ToString().Equals(paid)
                                   select p).FirstOrDefault();

                string pid = productdata.ProductId.ToString();

                var complaintdata = from p in db.tblComplaints
                                    where p.ProductId.ToString().Equals(pid)
                                    select p;

                ProductAdminView pav = new ProductAdminView { ProductAdmindata = productadmin, Complaintdata = complaintdata };

                return View(pav);
            }
            else
                return RedirectToAction("Login", "Login");
        }

            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public ActionResult Assign(int complaintid)
        {
            try
            {
                dbCMSEntities db = new dbCMSEntities();

                string sid = Session["Id"].ToString();
                Session["complaintid"] = complaintid;

                var comp = (from p in db.tblComplaints
                            where p.ComplaintId.Equals(complaintid) && (p.ComplaintStatus.Equals("Not Assigned") | p.ComplaintStatus.Equals("Feedback Provided"))
                            select p).FirstOrDefault();

                string pid = comp.ProductId.ToString();

                var prodadmin = (from p in db.tblProducts
                                where p.ProductId.ToString().Equals(pid) && p.ProductAdminId.ToString().Equals(sid)
                                select p).FirstOrDefault();

                var worker = from w in db.tblWorkers
                             where w.ProductAdminId.ToString().Equals(sid)
                             select w;
                return View(worker);
            }

            catch(Exception)
            {
                return RedirectToAction("Home", "ProductAdmin");
            }
        }

        [HttpPost]
        public ActionResult Assign(string WorkerId)
        {
            if (Session["LoginType"].Equals("ProductAdmin") && Session["Id"] != null)
            {
                string paid = Session["Id"].ToString();
                string cid = Session["complaintid"].ToString();
                dbCMSEntities db = new dbCMSEntities();

                try
                {
                    var worker = (from w in db.tblWorkers
                                  where w.WorkerId.ToString().Equals(WorkerId)
                                  select w).FirstOrDefault();

                    if (worker.ProductAdminId.ToString() != Session["Id"].ToString())
                    {
                        ModelState.AddModelError("Error", "Invalid Worker Id");
                        var workernew = from w in db.tblWorkers
                                     where w.ProductAdminId.ToString().Equals(paid)
                                     select w;
                        return View(workernew);
                    }

                    var comp = (from i in db.tblComplaints
                                where i.ComplaintId.ToString().Equals(cid)
                                select i).FirstOrDefault();
                    comp.WorkerId = worker.WorkerId;
                    comp.ComplaintStatus = "Assigned";
                    worker.NoOfCases = worker.NoOfCases + 1;
                    worker.CurrentStatus = "Working";
                    db.SaveChanges();
                    return RedirectToAction("Home", "ProductAdmin");
                }
                catch (System.NullReferenceException)
                {
                    ModelState.AddModelError("Error", "Invalid Worker Id");
                    var worker = from w in db.tblWorkers
                                 where w.ProductAdminId.ToString().Equals(paid)
                                 select w;
                    return View(worker);
                }
                catch (System.InvalidOperationException)
                {
                    ModelState.AddModelError("Error", "Invalid Worker Id");
                    var worker = from w in db.tblWorkers
                                 where w.ProductAdminId.ToString().Equals(paid)
                                 select w;
                    return View(worker);
                }
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        public ActionResult Close(string complaintid)
        {
            dbCMSEntities db = new dbCMSEntities();

            try
            {
                if (Session["LoginType"].Equals("ProductAdmin") && Session["Id"] != null)
                {
                    int cid = int.Parse(complaintid);

                    var comobj = (from c in db.tblComplaints
                                  where c.ComplaintId.Equals(cid)
                                  select c).FirstOrDefault();

                    comobj.ComplaintStatus = "Closed";
                    db.SaveChanges();

                    return RedirectToAction("Home", "ProductAdmin");
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