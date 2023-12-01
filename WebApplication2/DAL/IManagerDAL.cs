using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.DAL
{
    public interface IManagerDAL
    {
        DataTable GetEnrollemnt(Trainning trainning, User user, Enrollment enrollment);
        bool ApproveTrainning();
        bool DeclineTraining();

    }
}
