using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LOGIN_01.Models
{
    public class ResetPasswordModel
    {
        
        [Required(ErrorMessage = "必要輸入", AllowEmptyStrings = false)]
        [StringLength(16, ErrorMessage = "密碼必須8~16個字元!", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "新的密碼")]
        public string NewPassword { get; set; }

       
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "請重新確認密碼是否相同")] 
        [Display(Name = "密碼確認")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string ResetCode { get; set; }
    }
}