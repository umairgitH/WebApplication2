using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeTrainningClassLibrary.Models
{
    public class Trainning
    {
        public int TrainingId { get; set; }
        public string TrainingName { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public string PriorityDepartment {get; set; }
    }
}