using EmployeeTrainningClassLibrary.BusinessLayer;
using EmployeeTrainningClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using WebApplication2.Roles;
using WebApplication2.SessionManagement;

namespace WebApplication2.Controllers
{
    [SessionTimeOut]
    public class AdminController : Controller
    {
        private readonly ITrainningService _trainingService;
        private readonly IEnrollmentService _enrollmentService;
        public AdminController(ITrainningService trainingService, IEnrollmentService enrollmentService)
        {
            _trainingService = trainingService;
            _enrollmentService = enrollmentService;
        }
        [CustomAuthorization("Admin")]
        public ActionResult AdminDashBoard()
        {
            List<Trainning> trainnings = _trainingService.GetTrainningList();
            return View(trainnings);
        }
        [HttpPost]
        [CustomAuthorization("Admin")]
        public ActionResult AddTrainning(Trainning trainning) {
            bool isTrainngAdded = _trainingService.AddTraining(trainning);
            if (isTrainngAdded)
            {
                return Json(new { result = "Success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { result = "Failed" }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        [CustomAuthorization("Admin")]
        public ActionResult UpdateTrainning(string attributeName, Trainning trainning, int trainningId)
        {
            string[] parameterToInclude = { attributeName };
            bool isTrainningUpdated = _trainingService.UpdateTrainnig(trainning, parameterToInclude,trainningId);
            if (isTrainningUpdated)
            {
                return Json(new { result = "Success" },JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { result = "Failed" }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        [CustomAuthorization("Admin")]
        public async Task <ActionResult> DeleteTrainning(int trainningId)
        {
            bool isTrainningDeleted = await _trainingService.DeleteTrainningAsync(trainningId);
            if(isTrainningDeleted)
            {
                return Json(new { result = "Success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { result = "Cannot delete" }, JsonRequestBehavior.AllowGet);
            }
        }
        [CustomAuthorization("Admin")]
        public async Task <ActionResult> DisplayEnrollment(int trainningId)
        {           
            bool isSelectionDone = await _enrollmentService.checkForSelectionDoneAsync(trainningId);
            if (!isSelectionDone)
            {
                Session["trainningId"] = trainningId;
                List<EnrollmentOfEmployee> enrollmentOfEmployeesList = await _enrollmentService.GetEmployeeEnrollmentAsync(trainningId);
                return View(enrollmentOfEmployeesList);
            }
            else
            {
                return Json(new { result = "Already done" }, JsonRequestBehavior.AllowGet);
            }                   
        }
        [HttpPost]
        [CustomAuthorization("Admin")]
        public ActionResult ProcessEnrollment()
        {
            int trainningId = (int)Session["trainningId"];
            bool isSelectionDone =  _enrollmentService.ProcessEmployeeEnrollment(trainningId);
            if (isSelectionDone)
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