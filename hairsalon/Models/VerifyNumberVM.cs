using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace hairsalon.Models
{
    public class VerifyNumberVM
    {
        [Required(ErrorMessage ="لطفاً کد ارسال شده به شماره خود را وارد کنید")]
        [MaxLength(5)]
        public string VerificationCode { get; set; }
    }
}