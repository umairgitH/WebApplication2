namespace EmployeeTrainningClassLibrary.Models
{
    public class FileStorage
    {
        public int FileId { get; set;}
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
    }
}
