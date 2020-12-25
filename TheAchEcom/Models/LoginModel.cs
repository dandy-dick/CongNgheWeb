using System;
using System.Collections.Generic;
using Repository.DomainModels;
using Repository.BusinessModels;
using Repository.BusinessModels.ShopList;
using System.ComponentModel.DataAnnotations;

namespace TheAchEcom.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Làm ơn không để trống")]
        [MinLength(3, ErrorMessage = "Tên đăng nhập phải từ 6 kí tự - 40 kí tự")]
        [StringLength(40, ErrorMessage = "Tên đăng nhập phải từ 6 kí tự - 40 kí tự")]
        [Display(Name = "Tên đăng nhập")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Làm ơn không để trống")]
        [MinLength(3, ErrorMessage = "Mật khẩu phải từ 6 kí tự trở lên")]
        [DataType("Password")]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [Display(Name = "Tự động đăng nhập")]
        public bool RememberMe { get; set; } = true;
    }
}
