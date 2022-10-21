using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace hairsalon.Models
{
    public class ChangePassVM
    {
        [Required(ErrorMessage = "لطفا رمز خود را وارد کنید")]
        [RegularExpression("(?=.*[A-Za-z])(?=.*[1234567890])[A-Za-z1234567890]{8,}", ErrorMessage = "رمز باید حداقل 8 رقم، و دارای حرف و عدد باشد")]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "لطفا رمز جدید خود را وارد کنید")]
        [RegularExpression("(?=.*[A-Za-z])(?=.*[1234567890])[A-Za-z1234567890]{8,}", ErrorMessage = "رمز باید حداقل 8 رقم، و دارای حرف و عدد باشد")]
        public string NewPassword { get; set; }
       
        [Required(ErrorMessage = "لطفا تکرار رمز جدید را وارد کنید")]
        [Compare("NewPassword", ErrorMessage = "تکرار رمز با رمز وارد شده مطابق نیست")]
        public string NewRePassword { get; set; }
    }
}