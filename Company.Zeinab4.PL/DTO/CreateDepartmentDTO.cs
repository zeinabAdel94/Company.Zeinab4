using System.ComponentModel.DataAnnotations;

namespace Company.Zeinab4.PL.DTO
{
    public class CreateDepartmentDTO
    {
        [Required(ErrorMessage ="Code Is Required !")]
        public string Code { get; set; }
        [Required(ErrorMessage ="Name Is Required !")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Date Of Creation IS Required !")]
        public DateTime CreateAt { get; set; } 
    }
}
