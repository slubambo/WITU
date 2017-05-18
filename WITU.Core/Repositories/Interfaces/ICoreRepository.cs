using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Criterion;

namespace WITU.Core.Repositories.Interfaces
{
    public interface ICoreRepository
    {
        /// <summary>
        /// Saves or Commits the changes made of the entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool SaveOrUpdate(object entity);
        
        /// <summary>
        /// Saving an item without a transaction
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool SaveOrUpdateWithoutTransaction(object entity);

        /// <summary>
        /// Saves a new entity to the database
        /// </summary>
        /// <param name="entity">Object to be saved</param>
        /// <returns>Id of saved object otherwise returns -1 incase save fails</returns>
        object Save(object entity);

        /// <summary>
        /// Mostly used for updating as an alternative to <see cref="SaveOrUpdate"/>
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Merge(object entity);

        /// <summary>
        /// Saves a list of entities that are coming from a collection. Works similar to <see cref="SaveOrUpdate"/> Method,
        /// only that one requires to specify the entity for saving.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <returns></returns>
        bool SaveOrUpdateAll<T>(IEnumerable<T> entities) where T : class;

        /// <summary>
        /// Deletes the entity object passed on to it.
        /// </summary>
        /// <param name="entity">Entity to be deleted!!</param>
        /// <returns>True if the object was actually delete, false otherwise!!</returns>
        bool Delete(object entity);

        /// <summary>
        /// Deletes the Entity passed to it.
        /// </summary>
        /// <typeparam name="T">Entity of Type T to be deleted</typeparam>
        /// <param name="id">Primary Key of the entity</param>
        /// <returns>True if the process was a success, false otherwise</returns>
        bool Delete<T>(int id) where T : class;

        /// <summary>
        /// Delets a list of ids from an entity in a given entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool Delete<T>(IList<int> ids) where T : class;

        /// <summary>
        /// Retrieves all data of the entity class type passed.
        /// </summary>
        /// <typeparam name="T">ORM Entity</typeparam>
        /// <returns></returns>
        /// 

        IEnumerable<T> GetAll<T>() where T : class;

        /// <summary>
        /// Retrieves all data of the entity class type passed.
        /// </summary>
        /// <typeparam name="T">ORM Entity</typeparam>
        /// <returns></returns>
        /// 

        IEnumerable<T> GetAll<T>(List<int> ids) where T : class;

        /// <summary>
        /// Retrieves the data corresponding to the primary key integer presented to it.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get<T>(int id) where T : class;

        T Get<T>(string name, object value) where T : class;

        int CountAll<T>() where T : class;

        /// <summary>
        /// Saves and updates two entities, one probably an older one while the other representing the new records.
        /// </summary>
        /// <typeparam name="T">Database ORM class type</typeparam>
        /// <param name="currentEntity">Entity to be saved, most probably added</param>
        /// <param name="oldEntity">Entity to be retired, probably the old db.</param>
        /// <returns></returns>
        bool SaveOrUpdateOverwrite<T>(T currentEntity, T oldEntity = null) where T : class;
    }
}
