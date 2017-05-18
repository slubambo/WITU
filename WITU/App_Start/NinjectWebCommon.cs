using System.Reflection;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WITU.Core;
using WITU.Core.Helper.Impls;
using WITU.Core.Helper.Interfaces;
using WITU.Core.Repositories.Impl;
using WITU.Core.Repositories.Interfaces;
using WITU.Helper.Impls;
using WITU.Helper.Interfaces;
using WITU.Models;
using WITU.Utils;
using FluentValidation;
using FluentValidation.Mvc;
using Ninject.Web.Mvc.FilterBindingSyntax;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(WITU.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(WITU.App_Start.NinjectWebCommon), "Stop")]

namespace WITU.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            //loading the repository module...
            kernel.Load(new RepositoryModule());

            //var ninjectValidatorFactory = new NinjectValidatorFactory(kernel);
            //ModelValidatorProviders.Providers.Add(new FluentValidationModelValidatorProvider(ninjectValidatorFactory));
            //DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
            //FluentValidationModelValidatorProvider.Configure(x => x.ValidatorFactory = ninjectValidatorFactory);

            AssemblyScanner.FindValidatorsInAssembly(Assembly.GetExecutingAssembly())
                .ForEach(match => kernel.Bind(match.InterfaceType).To(match.ValidatorType));

            //bindings..
            kernel.Bind<ICoreRepository>().To<CoreRepository>();
            kernel.Bind<IAccountRepository>().To<AccountRepository>();
            kernel.Bind<IAdminUnitRepository>().To<AdminUnitRepository>();
            kernel.Bind<IGeneralHelper>().To<GeneralHelper>();
            kernel.Bind<IValidationHelper>().To<ValidationHelper>();
            kernel.Bind<IStudentRepository>().To<StudentRepository>();
            kernel.Bind<IStaffRepository>().To<StaffRepository>();
            kernel.Bind<IAuditRepository>().To<AuditRepository>();
            kernel.Bind<ISemesterRepository>().To<SemesterRepository>();
            kernel.Bind<IAcademicYearRepository>().To<AcademicYearRepository>();
            kernel.Bind<IUserManagementRepository>().To<UserManagementRepository>();
            kernel.Bind<ISemesterRegistrationRepository>().To<SemesterRegistrationRepository>();
            kernel.Bind<IStudentCourseRepository>().To<StudentCourseRepository>();
            kernel.Bind<IOauthRepository>().To<OauthRepository>();
			kernel.Bind<IStaffCourseRepository>().To<StaffCourseRepository>();
            kernel.Bind<IStudentsHelper>().To<StudentsHelper>();
            kernel.Bind<ICourseRepository>().To<CourseRepository>();

            kernel.BindFilter<AuditAttribute>(FilterScope.Action, 0).WhenActionMethodHas<AuditAttribute>().InRequestScope();
            kernel.BindFilter<AuthorizeUserAttribute>(FilterScope.Action, 0)
                .WhenActionMethodHas<AuthorizeUserAttribute>().InRequestScope();
        }        
    }
}
