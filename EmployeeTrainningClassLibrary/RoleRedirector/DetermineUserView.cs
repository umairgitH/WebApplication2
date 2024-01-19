using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTrainningClassLibrary.RoleRedirector
{
    public class DetermineUserView
    {
        //key value pair
        private static readonly Dictionary<string, (string controller, string action)> roleMapping
            = new Dictionary<string, (string controller, string action)>
            {
                {"Employee", ("Trainning","Trainning") },
                {"Manager", ("Manager","SelectTrainning") },
                {"Admin",("Admin","AdminDashBoard") }
            };
        public static (string controller, string action) GetRedirectionInfo(string roleName)
        {
            //get the values associated with the key
            if (roleMapping.TryGetValue(roleName, out var roleInfo))
            {
                return roleInfo;
            }
            return GetDefaultRedirectInfo();
        }
        private static (string controller, string action) GetDefaultRedirectInfo()
        {
            return ("Home", "Index");
        }
    }
}
