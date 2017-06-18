using ComplaintManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComplaintManagementSystem.Controllers
{
    public class WorkerController : Controller
    {
        public ActionResult Home()
        {
            try
            {
                if (Session["LoginType"].Equals("Worker") && Session["Id"] != null)
                {
                    dbCMSEntities db = new dbCMSEntities();
                    string sessionid = Session["Id"].ToString();
                    var worker = (from work in db.tblWorkers
                                  where work.WorkerId.ToString().Equals(sessionid)
                                  select work).FirstOrDefault();

                    var complaintdata = from comp in db.tblComplaints
                                        where comp.WorkerId.ToString().Equals(sessionid)
                                        select comp;

                    WorkerView wv = new WorkerView { Workerdata = worker, Complaintdata = complaintdata };

                    return View(wv);
                }
                else
                    return RedirectToAction("Login", "Login");
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult AskQuery(string complaintid)
        {
            dbCMSEntities db = new dbCMSEntities();
            try
            {
                if (Session["LoginType"].Equals("Worker") && Session["Id"] != null)
                {
                    string cid = complaintid;
                    string sid = Session["Id"].ToString();

                    var comobj = (from c in db.tblComplaints
                                  where (c.ComplaintId.ToString().Equals(cid) && c.ComplaintStatus.Equals("Assigned") && c.WorkerId.ToString().Equals(sid))
                                  select c).FirstOrDefault();
                    if(comobj==null)
                    {
                        return RedirectToAction("Home", "Worker");
                    }
                    else
                    {
                        return View(comobj);
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
        public ActionResult AskQuery(tblComplaint comobj)
        {
            try
            {
                if (Session["LoginType"].Equals("Worker") && Session["Id"] != null)
                {
                    dbCMSEntities db = new dbCMSEntities();

                    var obj = (from c in db.tblComplaints
                               where c.ComplaintId.Equals(comobj.ComplaintId)
                               select c).FirstOrDefault();

                    obj.WorkerQuery = comobj.WorkerQuery;
                    obj.CustomerReply = "Awaiting Reply";
                    db.SaveChanges();
                    return RedirectToAction("Home", "Worker");
                }
                else
                    return RedirectToAction("Login", "Login");
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult Resolved(string complaintid)
        {
            dbCMSEntities db = new dbCMSEntities();

            try
            {
                if (Session["LoginType"].Equals("Worker") && Session["Id"] != null)
                {
                    int cid = int.Parse(complaintid);
                    string wid = Session["Id"].ToString();

                    var comobj = (from c in db.tblComplaints
                                  where c.ComplaintId.Equals(cid)
                                  select c).FirstOrDefault();

                    comobj.ComplaintStatus = "Resolved";
                    comobj.WorkerQuery = "NA";
                    comobj.CustomerReply = "NA";

                    var worker = (from w in db.tblWorkers
                                  where w.WorkerId.ToString().Equals(wid)
                                  select w).FirstOrDefault();

                    if (worker.NoOfCases == 1)
                        worker.CurrentStatus = "Free";

                    worker.NoOfCases = worker.NoOfCases - 1;

                    db.SaveChanges();

                    return RedirectToAction("Home", "Worker");
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