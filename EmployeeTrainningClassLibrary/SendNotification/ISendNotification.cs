using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTrainningClassLibrary.SendNotification
{
    public interface ISendNotification
    {
        Task SendNotificationAsync(string recipient, string subject, string message);
    }
}
