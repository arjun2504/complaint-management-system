//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ComplaintManagementSystem.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblWorker
    {
        public tblWorker()
        {
            this.tblComplaints = new HashSet<tblComplaint>();
        }
    
        public int WorkerId { get; set; }
        public string Pssd { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nullable<int> ProductAdminId { get; set; }
        public string CurrentStatus { get; set; }
        public Nullable<int> NoOfCases { get; set; }
    
        public virtual ICollection<tblComplaint> tblComplaints { get; set; }
        public virtual tblProductAdmin tblProductAdmin { get; set; }
    }
}