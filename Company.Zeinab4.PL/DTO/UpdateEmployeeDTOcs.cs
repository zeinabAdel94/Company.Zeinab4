using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Xunit.Abstractions;

namespace Company.Zeinab4.PL.DTO
{
    public class UpdateEmployeeDTOcs
    {
        [Required(ErrorMessage = "Name Is Required")]
        public string Name { get; set; }
        [Range(22, 60, ErrorMessage = "Age Must Be Between 22 ,60")]
        public int? Age { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "Email  Is  Invaild ")]
        public string Email { get; set; }

        [RegularExpression(@"[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",
            ErrorMessage = "Address Must Be Like 123-Street-City-Country ")]
        public string Address { get; set; }

        [Phone]
        public string phone { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        [Required(ErrorMessage = "Active  Is Required")]
        public bool IsActive { get; set; }
        [Required(ErrorMessage = "Deleted  Is Required")]
        public bool IsDeleted { get; set; }

        [DisplayName("Hiring Date ")]
        [Required(ErrorMessage = "HiringDate Is Required")]
        public DateTime HiringDate { get; set; }
        [DisplayName("Date Of Creation ")]
        [Required(ErrorMessage = "Create At  Is Required")]
        public DateTime CreateAt { get; set; }
        public string DepartmentName { get; set; }
        public int? DepartmentId { get; set; }
        public string? ImageName { get; set; }
        public IFormFile? Image { get; set; }


    }
}
