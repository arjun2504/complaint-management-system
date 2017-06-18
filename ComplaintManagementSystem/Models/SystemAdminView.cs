using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplaintManagementSystem.Models
{
    public class SystemAdminView
    {
        public IEnumerable<tblComplaint> complaintdata { get; set; }
        public IEnumerable<tblCustomer> customerdata { get; set; }
        public IEnumerable<tblProductAdmin> productadmindata { get; set; }
        public IEnumerable<tblWorker> workerdata { get; set; }
        public tblSystemAdmin systemadmindata { get; set; }
    }
}