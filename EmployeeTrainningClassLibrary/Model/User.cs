using EmployeeTrainningClassLibrary.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace EmployeeTrainningClassLibrary.Models
{
    public class User
    {
        public int UserId { get; }
        [Required(ErrorMessage = "First name is required")]
        public string FirstName {  get; set; }
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Phone number is required")]
        public int PhoneNum {  get; set; }
        [Required (ErrorMessage = "Email is required")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage ="Invalid Email address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*[0-9])(?=.*[^a-zA-Z0-9]).{8,}$", ErrorMessage = "Weak Password! Enter a stronger password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Manager name is required")]
        public string ManagerName {  get; set; }
        public string Department {  get; set; }
        [Required(ErrorMessage = "NIC is required")]
        public string NIC { get; set; }
        public int RoleId { get; }
    }
}