using Models.Enums;

namespace Models.DTOs
{
    public class EmployeeDTO
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => FirstName + " " + LastName;
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public double Salary { get; set; }
    }
}
