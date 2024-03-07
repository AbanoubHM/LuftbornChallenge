using System.ComponentModel.DataAnnotations;

namespace LuftbornChallenge.DTO {
    public class EmployeeDto {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public DateTime Dob { get; set; }
        public string Gender { get; set; }
        public string Education { get; set; }
        public int Experience { get; set; }
    }
}
