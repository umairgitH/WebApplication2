using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Mvc5;
using EmployeeTrainningClassLibrary.BusinessLayer;
using EmployeeTrainningClassLibrary.DAL;

namespace WebApplication2
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IDataAccessLayer, DataAccessLayer>();
            container.RegisterType<IRetrieveUserForAuthenticationDAL, RetrieveUserForAuthenticationDAL>();
            container.RegisterType<IUserDAL, UserDAL>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<ITrainningService, TrainningService>();
            container.RegisterType<ITrainningDAL, TrainningDAL>();
            container.RegisterType<IEnrollmentDAL, EnrollmentDAL>();
            container.RegisterType<IEnrollmentService, EnrollmentService>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
