using System;
using NHibernate;
using NHibernate.Cfg;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;

namespace WITU.Core
{
    [Obsolete("Not in use! Use RepositoryModule instead")]
	public static partial class SessionManager
	{
		private static readonly ISessionFactory _sessionFactory;

		static SessionManager()
		{
			Configuration cfg = new Configuration().Configure();
			_sessionFactory = Fluently.Configure(cfg)
				.Mappings(m => m.FluentMappings.AddFromAssembly(typeof(SessionManager).Assembly))
				.BuildSessionFactory();
		}

		public static ISession OpenSession()
		{
			return _sessionFactory.OpenSession();
		}

		public static ISessionFactory SessionFactory
		{
			get { return _sessionFactory; }
		}
	}
}