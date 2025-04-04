using System.ComponentModel.DataAnnotations;

namespace Company.Zeinab4.PL.DTO
{
    public class ResetPasswordDTO
    {

        [Required(ErrorMessage = "Password Is Required")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }


        [Required(ErrorMessage = "ConfirmPassword Is Required")]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = "ConfirmPassWord does not Match ")]
        public string ConfirmPassword { get; set; }

    }
}
