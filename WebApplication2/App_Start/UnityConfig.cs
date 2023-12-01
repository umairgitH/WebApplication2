using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Mvc5;
using WebApplication2.BL;
using WebApplication2.Controllers;
using WebApplication2.DAL;

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
            container.RegisterType<IuserLoginDAL, UserDAL>();
            container.RegisterType<IEmployeeDAL, EmployeeDAL>();
            container.RegisterType<IManagerDAL, ManagerDAL>();
            container.RegisterType<IAdminDAL, AdminDAL>();
            container.RegisterType<IEmployeeService, EmployeeService>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
