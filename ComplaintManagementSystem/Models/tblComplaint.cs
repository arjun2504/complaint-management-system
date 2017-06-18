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
    using System.ComponentModel.DataAnnotations;
    
    public partial class tblComplaint
    {
        public int ComplaintId { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public Nullable<int> WorkerId { get; set; }
        public Nullable<int> ProductId { get; set; }
        public string ComplaintText { get; set; }
        [Required(ErrorMessage = "Please enter your query.")]
        public string WorkerQuery { get; set; }
        [Required(ErrorMessage = "Please enter your reply.")]
        public string CustomerReply { get; set; }
        public string ComplaintStatus { get; set; }
        public Nullable<System.DateTime> ComplaintDate { get; set; }
        [Required(ErrorMessage = "Please enter your feedback.")]
        public string Feedback { get; set; }
    
        public virtual tblCustomer tblCustomer { get; set; }
        public virtual tblProduct tblProduct { get; set; }
        public virtual tblWorker tblWorker { get; set; }
    }
}