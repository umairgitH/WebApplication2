using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTrainningClassLibrary.Models
{
    public class EnrollmentOfEmployee
    {
        public short EnrollementId {  get; set; }
        public string Status {  get; set; }
        public byte FileId {  get; set; }
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public string Department {  get; set; }
        public string FileName { get; set; }
        public string ContentType {  get; set; }
        public byte[] Data { get; set; }
    }
}
