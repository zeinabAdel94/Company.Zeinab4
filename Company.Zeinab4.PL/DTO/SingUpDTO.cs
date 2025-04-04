using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Company.Zeinab4.PL.DTO
{
    public class SingUpDTO
    {
        [Required(ErrorMessage ="UserName Is Required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "FirstName Is Required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName Is Required")]
        public string  LastName { get; set; }

        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress]
        public string Email { get; set; }


        [Required(ErrorMessage = "Password Is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required(ErrorMessage = "ConfirmPassword Is Required")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password),ErrorMessage ="ConfirmPassWord does not Match ")]
        public string ConfirmPassword { get; set; }


        public bool IsAgree { get; set; }
    }
}
