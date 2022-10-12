using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace hairsalon.Models
{
    public class CheckPhoneVM
    {

        [Required(ErrorMessage ="لطفا شماره موبایل خود را وارد کنید.")]
        [RegularExpression("09-?[012349]-?[0-9]{8}", ErrorMessage ="شماره موبایل نامعتبر است.")]
        public string PhoneNumber { get; set; }

    }
}