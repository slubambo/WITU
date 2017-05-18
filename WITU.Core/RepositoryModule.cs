using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WITU.Core.Utils;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cache;
using NHibernate.Caches.SysCache;
using NHibernate.Dialect;
using NHibernate.Tool.hbm2ddl;
using Ninject;
using Ninject.Infrastructure;
using Ninject.Modules;
using Ninject.Web.Common;

namespace WITU.Core
{
    public class RepositoryModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ISessionFactory>()
                .ToMethod
                (
                    e =>
                        Fluently.Configure()
                        .ExposeConfiguration(x => x.SetInterceptor(new QueryInterceptor()))
                        .Database(
                            MySQLConfiguration.Standard.ShowSql().ConnectionString(c =>
                                c.FromConnectionStringWithKey("DefaultConnection"))
                                .Dialect<MySQL5Dialect>())
                        .Cache(c => 
                            //c.UseQueryCache().ProviderClass<HashtableCacheProvider>()
                            c.UseQueryCache().UseSecondLevelCache().ProviderClass<SysCacheProvider>()
                            )
                        .Diagnostics(diag => diag.Enable().OutputToConsole())
                        .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()).Conventions.AddFromAssemblyOf<EnumConvention>()
                            //.Add<ConditionFilter>()
                            //.ExportTo(@"C:\Users\agabadave\Project\CSharp\2012\PSP\PSP.Corse\GeneratedXmlMappings")
                            )
                        //.ExposeConfiguration(x => x.SetProperty("current_session_context_class", "web"))
                        //.ExposeConfiguration(x => new SchemaUpdate(x).Execute(false, true))
                        .BuildSessionFactory()
                )
                .InSingletonScope();
            Bind<ISession>()
                .ToMethod((ctx) => ctx.Kernel.Get<ISessionFactory>().OpenSession())
                .InRequestScope();



            //Bind<ISession>()
            //    .ToMethod((ctx) => ctx.Kernel.Get<ISessionFactory>().OpenSession())
            //    .When(x => HttpContext.Current == null)
            //    .InThreadScope();

            //Bind<ISession>()
            //    .ToMethod((ctx) => ctx.Kernel.Get<ISessionFactory>().OpenSession())
            //    .When(x => HttpContext.Current != null)
            //    .InRequestScope();
        /*    Bind<ISession>()
                .ToMethod((ctx) => ctx.Kernel.Get<ISessionFactory>().OpenSession())
                .InScope(ctx => StandardScopeCallbacks.Request(ctx) ?? StandardScopeCallbacks.Thread(ctx)); */
        }
    }
}
