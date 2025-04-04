using System.ComponentModel.DataAnnotations;

namespace Company.Zeinab4.PL.DTO
{
    public class ForgetPasswordDTO
    {


        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
