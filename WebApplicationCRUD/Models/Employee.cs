using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationCRUD.Models
{
    [Table("Employee", Schema = "HR")]
    public class Employee
    {
        [Key]
        [Display(Name = "ID")]
        public int? EmployeeId { get; set; }

        [Required]
        [Display(Name = "Name")]
        [Column(TypeName = "varchar(250)")]
        public string EmployeeName { get; set; } = string.Empty;

        [Display(Name = "User Iamge")]
        [Column(TypeName = "varchar(250)")]
        public string? UserImage { get; set; }

        [Display(Name = "Birth Date")][DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Display(Name ="Salary")]
        [Column(TypeName="decimal(12,2)")]
        public decimal Salary { get; set; }

        [Display(Name ="Hiring Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd-MMMM-yyyy}")]
        public DateTime HiringDate { get; set; }

        [Required]
        [Display(Name = "National ID")]
        [MaxLength(14)]
        [MinLength(14)]
        [Column(TypeName = "varchar(14)")]
        public string NationalId { get; set; }

        public int DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public Department? Department { get; set; }
    }
}
