using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplaintManagementSystem.Models
{
    public class ProductAdminView
    {
        public tblProductAdmin ProductAdmindata { get; set; }
        public IEnumerable<tblComplaint> Complaintdata { get; set; }
        public tblProduct Productdata { get; set; }
    }
}
