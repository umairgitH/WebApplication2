using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.BL
{
    public interface IEmployeeService
    {
        bool RegisterEmployee(User user);
        //DataTable GetUser(User user);
        bool GetUser(User user);

        bool Enroll(User user, Trainning trainning, Enrollment enrollment);
        DataTable DisplayTrainning(Trainning trainning);
    }
}
