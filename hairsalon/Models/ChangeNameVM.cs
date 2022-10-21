using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace hairsalon.Models
{
    public class ChangeNameVM
    {
        [Required(ErrorMessage ="لطفا نام خود را وارد کنید.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage ="لطفا نام خانوادگی خود را وارد کنید.")]
        public string LastName { get; set; }
    }
}