using EmployeeTrainningClassLibrary.BusinessLayer;
using EmployeeTrainningClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Roles;
using System.Drawing.Imaging;

namespace WebApplication2.Controllers
{
    public class TrainningController : Controller
    {
        private readonly ITrainningService _trainingService;
        private readonly IEnrollmentService _enrollmentService;
        public TrainningController(ITrainningService trainningService, IEnrollmentService enrollmentService) { 
            _trainingService = trainningService;
            _enrollmentService = enrollmentService;
        }
        [HttpGet]
        [CustomAuthorization("Employee")]
        public ActionResult Trainning()
        {
            int userId = (int)Session["UserId"];
            if(Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                List<Trainning> trainningList = _trainingService.DisplayTrainning(userId);
                return View(trainningList);
            }
        }
        public ActionResult Enroll(int trainingId, string trainningName)
        {

            var trainningModel = new Trainning()
            {
                TrainingId = trainingId,
                TrainingName = DecodeTrainningName(trainningName)
            };
            return View(trainningModel);
        }
        [HttpPost]
        public async Task<ActionResult> Enroll(HttpPostedFileBase file) {
            int trainingId = int.Parse(Request.Form["TrainingId"]);
            int userId = (int)Session["UserId"];

            bool IsEnrolled = _enrollmentService.IsEmployeeEnrolled(trainingId, userId, file);
            if (IsEnrolled)
            {
                ViewBag.Message = $"Your application has been successfully submitted.";
                await Task.Delay(3000);
                //return RedirectToAction("Trainning", "Trainning");
                return RedirectToAction("DiplayFileList", "Trainning");
            }
            else
            {
                ViewBag.Message = $"Error in submitting your application";
                await Task.Delay(3000);
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult DiplayFileList()
        {
            int userId = (int)Session["UserId"];
            var fileList = _enrollmentService.GetFileData(userId);

            if (fileList != null && fileList.Any())
            {
                return View(fileList);
            }
            else
            {
                return HttpNotFound();
            }
        }
        [HttpGet]
        public ActionResult Download(int fileId)
        {
            FileStorage file = _enrollmentService.GetFileDataToDownload(fileId);

            if (file != null)
            {
                return File(file.Data, file.ContentType, file.FileName);
            }

            return HttpNotFound();
        }
        static string DecodeTrainningName(string encodedString)
        {
            var regex = new Regex("..");

            var matches = regex.Matches(encodedString);

            var decodedString = new StringBuilder();
            foreach (Match match in matches)
            {
                string hexValue = match.Value;
                int charCode = Convert.ToInt32(hexValue, 16);
                decodedString.Append((char)charCode);
            }

            return decodedString.ToString();
        }
    }
}