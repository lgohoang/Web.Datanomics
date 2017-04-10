using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Web.Mvc;


namespace Web.Datanomics.Models
{
    public class ChangePassword
    {
        [Display(Name = "Old Password")]
        public string OldPassword { get; set; }

        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [Display(Name = "Re New Password")]
        public string ReNewPassword { get; set; }
    }
}
