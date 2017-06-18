using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplaintManagementSystem.Models
{
    public class CustomerView
    {
        public tblCustomer Customerdata { get; set; }
        public IEnumerable<tblComplaint> Complaintdata { get; set; }
    }
}