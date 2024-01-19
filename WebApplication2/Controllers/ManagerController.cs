using EmployeeTrainningClassLibrary.BusinessLayer;
using EmployeeTrainningClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Roles;
using WebApplication2.SessionManagement;

namespace WebApplication2.Controllers
{
    [SessionTimeOut]
    public class ManagerController : Controller
    {
        private readonly IEnrollmentService _enrollmentService;
        private readonly ITrainningService _trainningService;
        public ManagerController(IEnrollmentService enrollmentService, ITrainningService trainningService) { 
            _enrollmentService = enrollmentService;
            _trainningService = trainningService;
        }
        [CustomAuthorization("Manager")]
        public ActionResult SelectTrainning()
        {
            List<string> trainningNameList = _trainningService.GetAllTrainningName();
            ViewBag.TrainningName = trainningNameList;
            return View();
        }
        [HttpGet]
        [CustomAuthorization("Manager")]
        public async Task <ActionResult> DisplayTrainning(string trainningName)
        {
            int managerId = (int)Session["UserId"];
            var employeeEnrollmentList = await _enrollmentService.DiplayEnrollmentDetailsAsync(trainningName, managerId);
            return View(employeeEnrollmentList);
        }
        [HttpPost]
        public async Task <ActionResult> Download(int fileId)
        {
            FileStorage file = await _enrollmentService.GetFileDataToDownloadAsync(fileId);

            if (file != null)
            {
                return File(file.Data, file.ContentType, file.FileName);
            }

            return HttpNotFound();
        }
        [HttpPost]
        [CustomAuthorization("Manager")]
        public async Task <ActionResult> ApproveODeclineEnrollment(int enrollementId, string action)
        {
            bool isApproveOrDecline = await _enrollmentService.ApproveOrDeclineTrainningAsync(enrollementId, action);
            if (isApproveOrDecline)
            {
                return Json(new { result = "Success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { result = "Failed" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}