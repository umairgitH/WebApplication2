﻿using EmployeeTrainningClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeTrainningClassLibrary.Models
{
    public class Enrollment
    {
        public int EnrollementId { get; }
        public string ManagerApproval {  get; set; }
        public string EnrollmentDate {  get; set; }
        public string NIC { get; set; }
        public string Department { get; set; }
        public string DocumentPath {  get; set; }
        public string Status {  get; set; }
    }
}