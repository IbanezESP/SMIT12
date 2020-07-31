//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace 大專.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;

    public partial class Member
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Member()
        {
            this.Orders = new HashSet<Order>();
        }

        public int MemberID { get; set; }
        public string Uid { get; set; }
        [Required(ErrorMessage = "此欄位不能為空!")]
        [Display(Name = "密碼")]
        [StringLength(16, ErrorMessage = "密碼必須8~16個字元!", MinimumLength = 8)]
        public string Passwrod { get; set; }
        //[Required(ErrorMessage = "暱稱不能為空!")]
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public Nullable<System.DateTime> RegistrationTime { get; set; }
        public string PwdAnswer { get; set; }
        public string NickName { get; set; }
        public string Gender { get; set; }
        //[Required(ErrorMessage = "地址不能為空!")]
        public string Address { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }



        //public bool isChecked()
        //{
        //    bool flag = true;

        //    if (string.IsNullOrEmpty(this.Username))
        //    {
        //        flag = false;
        //        MyErrMessagr = "暱稱不能為空!";
        //    }
        //    if (string.IsNullOrEmpty(this.Address))
        //    {
        //        flag = false;
        //        MyErrMessagr = "地址不能為空!";
        //    }
        //    return flag;
        //}
    }
}
