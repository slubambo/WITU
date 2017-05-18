using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using WITU.API.App_Start;
using WITU.API.Providers;
using WITU.API.Providers.Impls;
using WITU.API.Providers.Interfaces;
using WITU.Core;
using WITU.Core.Helper.Impls;
using WITU.Core.Helper.Interfaces;
using WITU.Core.Repositories.Impl;
using WITU.Core.Repositories.Interfaces;
using FluentValidation;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.OAuth;
using Ninject;
using Ninject.Extensions.NamedScope;
using Ninject.Web.Common;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;

[assembly: OwinStartup(typeof(WITU.API.Startup))]
namespace WITU.API
{
    public class Startup
    {
        private readonly Lazy<IKernel> _kernel = new Lazy<IKernel>(() =>
        {
            var kernel = CreateKernel();
            return kernel;
        });
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            ConfigureOAuth(app); 
            app.UseNinjectMiddleware(() => _kernel.Value);
            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseNinjectWebApi(config);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            // Token Generation
            app.UseOAuthAuthorizationServer(_kernel.Value
       .Get<SimpleOAuthAuthorizationServerOptions>().GetOptions());
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
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
            kernel.Bind<IValidationHelper>().To<ValidationHelper>();
            kernel.Bind<IStudentRepository>().To<StudentRepository>();
            kernel.Bind<IStudentCourseRepository>().To<StudentCourseRepository>();
            kernel.Bind<IStaffRepository>().To<StaffRepository>();
            kernel.Bind<IAuditRepository>().To<AuditRepository>();
            kernel.Bind<ISemesterRepository>().To<SemesterRepository>();
            kernel.Bind<IAcademicYearRepository>().To<AcademicYearRepository>();
            kernel.Bind<IOauthRepository>().To<OauthRepository>();
            kernel.Bind<IOAuthAuthorizationServerOptions>()
            .To<SimpleOAuthAuthorizationServerOptions>();
            kernel.Bind<IOAuthAuthorizationServerProvider>()
                .To<SimpleAuthorizationServerProvider>();
            kernel.Bind<IAuthenticationTokenProvider>().To<SimpleRefreshTokenProvider>();
            kernel.Bind<IIdentityMessageService>().To<SmsService>();
            
        }        
    }
}