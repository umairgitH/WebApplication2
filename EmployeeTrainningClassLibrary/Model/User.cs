using EmployeeTrainningClassLibrary.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace EmployeeTrainningClassLibrary.Models
{
    public class User
    {
        public int UserId { get; }
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public int PhoneNum {  get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ManagerName {  get; set; }
        public int RoleId { get; }

    }
}