using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplaintManagementSystem.Models
{
    public class WorkerView
    {
        public tblWorker Workerdata { get; set; }
        public IEnumerable<tblComplaint> Complaintdata { get; set; }
    }
}