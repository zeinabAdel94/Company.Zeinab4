using System.ComponentModel.DataAnnotations;

namespace Company.Zeinab4.PL.DTO
{
    public class SingInDTO
    {

        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress]
        public string Email { get; set; }


        [Required(ErrorMessage = "Password Is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

     


        public bool RemmberMe { get; set; }    

    }
}
