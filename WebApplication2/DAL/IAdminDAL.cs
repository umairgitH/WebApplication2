using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.DAL
{
    public interface IAdminDAL : IuserLoginDAL
    {
        DataTable GetTrainnig(Trainning trainning);
        bool AddTrainnig(Trainning trainning);
        bool UpdateTrainnig(Trainning trainning);
        DataTable EmployeeSelection(User user, Trainning trainning, Enrollment enrollment);
    }
}
