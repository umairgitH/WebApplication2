using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Trainning
    {
        public int TrainingId { get;}
        public string Description { get; set; }
        public int Capacity {  get; set; }
        public string PriorityDepartment {  get; set; }

    }
}