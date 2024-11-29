using System.ComponentModel.DataAnnotations;

namespace EmployeeAdminPortal.Models
{
    public class UpdateEmployeeDto
    {
        public Guid Id { get; set; }
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
        public required string Name { get; set; }
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public required string Email { get; set; }
        [Phone(ErrorMessage = "Invalid phone number format")]
        public string? Phone { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive number")]
        public decimal Salary { get; set; }
    }
}
