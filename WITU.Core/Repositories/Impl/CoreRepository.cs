using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WITU.Core.Model;
using WITU.Core.Repositories.Interfaces;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;

namespace WITU.Core.Repositories.Impl
{
    public class CoreRepository : ICoreRepository
    {
        private readonly ISession _session;
        private readonly ISessionFactory _factory;

        public CoreRepository(ISession session)
        {
            _session = session;

            //applying filter of StudentCourse 
//            _session.EnableFilter("ExcludeDeletedStudentCourse").SetParameter("resultStatus", (int)ResultStatus.DeletedApproved);
        }

        public bool SaveOrUpdate(object entity)
        {
            var result = false;
            
            ////check if the session is closed..
            //if (!_session.IsOpen)
            //{
            //    _session.Connection.Open();
            //}

            using (var txn = _session.BeginTransaction())
            {
                try
                {
                    _session.SaveOrUpdate(entity);
                    txn.Commit();
                    result = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    txn.Rollback();
                    //_session.Close();
                }
            }
            //sess.Close();
            return result;

            //_session.SaveOrUpdate(entity);
            //result = true;
            //return result;
        }
        
        public bool SaveOrUpdateWithoutTransaction(object entity)
        {
            var result = false;

            _session.SaveOrUpdate(entity);
            _session.Flush();
            result = true;
            return result;
        }

        
        /// <summary>
        /// Saves new entity to the db
        /// </summary>
        /// <param name="entity">Entity to be saved</param>
        /// <returns>id of saved entity otherwise returns -1</returns>
        public object Save(object entity)
        {
            object result = -1;

            ////check if the session is closed..
            //if (!_session.IsOpen)
            //{
            //    _session.Connection.Open();
            //}

            using (var txn = _session.BeginTransaction())
            {
                try
                {
                    result = _session.Save(entity);
                    txn.Commit();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    txn.Rollback();
                    //_session.Close();
                }
            }
            //sess.Close();
            return result;

            //_session.SaveOrUpdate(entity);
            //result = true;
            //return result;
        }
      
        public bool Merge(object entity)
        {
            var result = false;
            using (var txn = _session.BeginTransaction())
            {
                try
                {
                    _session.Merge(entity);
                    txn.Commit();
                    result = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    txn.Rollback();
                }
            }
            return result;
        }

        public bool SaveOrUpdateAll<T>(IEnumerable<T> entities) where T : class
        {
            var result = false;
            using (var txn = _session.BeginTransaction())
            {
                try
                {
                    foreach (var et in entities)
                    {
                        _session.SaveOrUpdate(et);
                    }
                    txn.Commit();
                    result = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    txn.Rollback();
                }
            }
            return result;
        }


        public bool Delete(object entity)
        {
            var result = false;
            using (var txn = _session.BeginTransaction())
            {
                try
                {
                    _session.Delete(entity);
                    txn.Commit();
                    result = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    txn.Rollback();
                }
            }
            return result;
        }

        public bool Delete<T>(int id) where T : class
        {
            var result = false;
            using (var txn = _session.BeginTransaction())
            {
                try
                {
                    var queryString = string.Format("DELETE {0} WHERE id = :id", typeof (T));
                    _session.CreateQuery(queryString).SetParameter("id", id).ExecuteUpdate();
                    
                    txn.Commit();
                    result = true;
                }
                catch (Exception)
                {
                    txn.Rollback();
                }
            }
            return result;
        }

        public bool Delete<T>(IList<int> ids) where T : class
        {
            var result = false;
            using (var txn = _session.BeginTransaction())
            {
                try
                {
                    foreach (var i in ids)
                    {
                        var queryString = string.Format("DELETE {0} WHERE id = :id", typeof(T));
                        _session.CreateQuery(queryString).SetParameter("id", i).ExecuteUpdate();
                    }

                    txn.Commit();
                    result = true;
                }
                catch (Exception e)
                {
                    txn.Rollback();
                }
            }
            return result;
        }

        public IEnumerable<T> GetAll<T>() where T : class
        {
            var query = _session.CreateCriteria<T>()//Informing NHibernate that 
                //this particular query and its results are cach-able
            .SetCacheable(true)
                //There are various modes in which the 
                //second level cache could be used.
                //Here we are using the normal mode which means, 
                //NHibernate will first look for information in 
                //second level cache, 
                //If found it returns it.
                //If information is not found in the cache then 
                //it will hit the DB to get the information.
                //But before returning the results from the DB, 
                //it will also put the results in the Cache.
            .SetCacheMode(CacheMode.Normal);
            return query.List<T>();
        }

        public IEnumerable<T> GetAll<T>(List<int> ids ) where T : class
        {
            var query = _session.CreateCriteria<T>().Add(Restrictions.In(Projections.Id().ToString(), ids))//Informing NHibernate that 
                //this particular query and its results are cach-able
            .SetCacheable(true)
                //There are various modes in which the 
                //second level cache could be used.
                //Here we are using the normal mode which means, 
                //NHibernate will first look for information in 
                //second level cache, 
                //If found it returns it.
                //If information is not found in the cache then 
                //it will hit the DB to get the information.
                //But before returning the results from the DB, 
                //it will also put the results in the Cache.
            .SetCacheMode(CacheMode.Normal);
            return query.List<T>();
        }

        public T Get<T>(int id) where T : class
        {
            var query = _session.CreateCriteria<T>().Add(Restrictions.Eq(Projections.Id().ToString(), id)).SetCacheable(true).SetCacheMode(CacheMode.Normal);
            return query.UniqueResult<T>();
        }

        public T Get<T>(string name, object value) where T : class 
        {
            var query = _session.CreateCriteria<T>().Add(Restrictions.Eq(name, value)).SetCacheable(true).SetCacheMode(CacheMode.Normal);
            return query.UniqueResult<T>();
        }

        public int CountAll<T>() where T : class
        {
            return _session.Query<T>().Count();
        }

        public bool SaveOrUpdateOverwrite<T>(T currentEntity, T oldEntity = null) where T : class 
        {
            bool res = true;
            using (var txn = _session.BeginTransaction())
            {
                try
                {
                    //first we save the normal tution...
                    _session.SaveOrUpdate(currentEntity);
                    if (oldEntity != null)
                        _session.SaveOrUpdate(oldEntity);
                    txn.Commit();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    txn.Rollback();
                    res = false;
                }
            }

            return res;
        }

        public CohortYear GetAcademicYear(int startYear)
        {
            var academicYear = _session.Query<CohortYear>().Cacheable().CacheMode(CacheMode.Normal).FirstOrDefault(x => x.StartYear == startYear);
            return academicYear;
        }

        public Dictionary<int, CohortYear> EntryYearWithAcademicYears(int yearOfStudy, int academiYearId)
        {
            var dictionary = new Dictionary<int, CohortYear>();

            using (var txn = _session.BeginTransaction())
            {
                var defaultAcademicYear = Get<CohortYear>(academiYearId);

                for (int i = 1; i < yearOfStudy; i++)
                {
                    var joinedAcademicYar = GetAcademicYear((defaultAcademicYear.StartYear - yearOfStudy) + i);
                    dictionary.Add(i, joinedAcademicYar);
                }

                //finally we add the default academic year...
                dictionary.Add(yearOfStudy, defaultAcademicYear);
                txn.Commit();
            }

            return dictionary;
        }
    }
}
