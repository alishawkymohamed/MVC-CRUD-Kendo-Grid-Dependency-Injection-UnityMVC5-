using System.ComponentModel.DataAnnotations;

namespace Models.DbModels
{
    public class Employee : _BaseEntity
    {
        [Key]
        public int EmployeeId { get; set; }

        [StringLength(50, MinimumLength = 2)]
        [Required]
        public string FirstName { get; set; }

        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; }

        [Range(20, 60)]
        public int Age { get; set; }
        [StringLength(100, MinimumLength = 2)]
        public string Job { get; set; }
        [Required]
        public double Salary { get; set; }
    }
}
