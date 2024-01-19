using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Deadline { get; set; }
        public string PrerequisiteName {  get; set; }
    }
}