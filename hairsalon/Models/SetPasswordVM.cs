using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace hairsalon.Models
{
    public class SetPasswordVM
    {

        [Required(ErrorMessage = "لطفا رمز خود را وارد کنید")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$", ErrorMessage = "رمز باید حداقل 8 رقم، و دارای حداقل یک حرف، یک عدد و یک کاراکتر ویژه باشد")]
        public string Password { get; set; }
        [Required(ErrorMessage ="لطفا تکرار رمز را وارد کنید")]
        [Compare(nameof(Password))]
        public string RePassword { get; set; }
    }
}