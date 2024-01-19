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
using WebApplication2.SessionManagement;

namespace WebApplication2.Controllers
{
    [SessionTimeOut]
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
        public async Task <ActionResult> Trainning()
        {
            int userId = (int)Session["UserId"];
            if(Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                List<Trainning> trainningList = await _trainingService.DisplayTrainningAsync(userId);
                return View(trainningList);
            }
        }
        [HttpPost]
        public ActionResult Trainning(int trainingId, string trainningName)
        {
            Session["trainningId"] = trainingId;
            Session["TrainningName"] = trainningName;

            return Json(new { success = true });
        }
        [HttpGet]
        public async Task <ActionResult> Enroll()
        {
            int trainingId = (int)Session["trainningId"];
            string trainningName = Session["TrainningName"].ToString();
            string prerequisiteName = await _enrollmentService.GetPrerequisiteNameAsync(trainingId);
            var trainningModel = new Trainning()
            {
                TrainingId = trainingId,
                TrainingName = trainningName,
                PrerequisiteName = prerequisiteName
            };
            return View(trainningModel);
        }
        [HttpPost]
        public async Task <ActionResult> EnrollPost(HttpPostedFileBase file) {
            int trainingId = int.Parse(Request.Form["TrainingId"]);
            int userId = (int)Session["UserId"];
            bool IsEnrolled = await _enrollmentService.IsEmployeeEnrolledAsync(trainingId, userId, file);
            if (IsEnrolled)
            {
                return Json(new { result = "Success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { result = "Enrollment Failed" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}