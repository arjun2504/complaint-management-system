using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ComplaintManagementSystem.Models
{
    public class Login
    {
        [Required(ErrorMessage="Enter Email")]
        [RegularExpression(@"^([a-z0-9_\-\.]+)@((\[[0-9]{1,3}" + @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" + @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid Email Address.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter Password")]
        [Display(Name="Pssd")]
        [DataType(DataType.Password)]
        [RegularExpression(@"((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*_]).{6,20})", ErrorMessage = "Password should be 6-20 characters with at least one upper case letter, one lower case letter, one special character and a number.")]
        public string Pssd { get; set; }
        [Required(ErrorMessage = "Select Login Type")]
        public string LoginType { get; set; }
        [Required(ErrorMessage="Enter New Password")]
        [DataType(DataType.Password)]
        [Display(Name="NewPssd")]
        [RegularExpression(@"((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*_]).{6,20})", ErrorMessage = "Password should be 6-20 characters with at least one upper case letter, one lower case letter, one special character and a number.")]
        public string NewPssd { get; set; }
        [Required(ErrorMessage = "Enter Confirm Password")]
        [DataType(DataType.Password)]
        [Display(Name="ConfirmPssd")]
        [Compare("NewPssd",ErrorMessage="The new password and confirm password should match")]
        public string ConfirmPssd { get; set; }
    }
}