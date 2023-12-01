using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.DAL
{
    public interface IEmployeeDAL : IuserLoginDAL
    {
        bool RegisterEmployee(User user);
        bool Enroll(User user, Trainning trainning, Enrollment enrollment);
        DataTable DisplayTrainning(Trainning trainning);

    }
}
