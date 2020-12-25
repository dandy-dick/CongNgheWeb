using Repository.DomainModels;
using System;
using System.ComponentModel.DataAnnotations;
using TheAchEcom.Models.PayPal;

namespace TheAchEcom.Models
{
    public class OrderDetailModel 
    {
        [Required(ErrorMessage ="Làm ơn đừng để trống")]
        [Display(Name = "Tên người nhận")]
        [MinLength(2, ErrorMessage = "Phải từ 2 kí tự trở lên")]
        public string FullName { get; set; }

        [Required(ErrorMessage ="Làm ơn đừng để trống")]
        [MinLength(15, ErrorMessage = "Dường như bạn nhập sai địa chỉ, địa chỉ này quá ngắn")]
        [Display(Name = "Địa chỉ giao hàng")]
        public string DeliveryAddress { get; set; }
        
        [Required(ErrorMessage ="Làm ơn đừng để trống")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Số điện thoại không đúng định dạng")]
        [MinLength(8, ErrorMessage = "Số điện thoại không đúng định dạng")]
        [Display(Name = "Số điện thoại người nhận")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Mô tả thêm")]
        public string Description { get; set; }
    }

}
