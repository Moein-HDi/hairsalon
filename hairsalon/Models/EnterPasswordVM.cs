using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace hairsalon.Models
{
    public class EnterPasswordVM
    {
        [Required(ErrorMessage ="لطفا رمز عبور خود را وارد کنید")]
        public string Password { get; set; }
    }
}