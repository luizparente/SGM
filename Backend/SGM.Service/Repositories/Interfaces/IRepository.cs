using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orion.Service.Repositories.Interfaces {
    /// <summary>
    /// An interface that specifies the basic CRUD operation methods for all repositories.
    /// </summary>
    /// <typeparam name="T">The concrete type handled by the implementing repository.</typeparam>
    public interface IRepository<T> {
        /// <summary>
        /// Asynchronously creates a database record for the specified object in its corresponding table.
        /// </summary>
        /// <param name="obj">The object to be written.</param>
        /// <returns>A Task object representing the asynchronous operation.</returns>
        public Task CreateAsync(T obj);

        /// <summary>
        /// Asynchronously creates a set of database records for the specified objects in its corresponding table.
        /// </summary>
        /// <param name="objs">The collection of objects to be written.</param>
        /// <returns>A Task object representing the asynchronous operation.</returns>
        public Task CreateAsync(IEnumerable<T> objs);

        /// <summary>
        /// Asynchronously retrieves a database record for the specified object ID in its corresponding table.
        /// </summary>
        /// <param name="ID">The unique identifier of the desired object.</param>
        /// <returns>A Task object representing the asynchronous operation.</returns>
        public Task<T> GetAsync(object ID);

        /// <summary>
        /// Asynchronously retrieves all database records from the corresponding table.
        /// </summary>
        /// <returns>A Task object representing the asynchronous operation.</returns>
        public Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Asynchronously retrieves all database records from the corresponding table, limited by a number of records.
        /// </summary>
        /// <param name="max">The maximum number of records to be retrieved.</param>
        /// <returns>A Task object representing the asynchronous operation.</returns>
        public Task<IEnumerable<T>> GetAllAsync(int max);

        /// <summary>
        /// Asynchronously retrieves a collection of database record from the corresponding table based on the specified SQL query.
        /// </summary>
        /// <param name="query">An interpolated string containing the SQL query.</param>
        /// <returns>A Task object representing the asynchronous operation.</returns>
        public Task<IEnumerable<T>> GetWithQueryAsync(FormattableString query);

        /// <summary>
        /// Asynchronously updates a database record for the specified object in its corresponding table.
        /// </summary>
        /// <param name="obj">The object to be updated.</param>
        /// <returns>A Task object representing the asynchronous operation.</returns>
        public Task UpdateAsync(T obj);

        /// <summary>
        /// Asynchronously deletes a database record for the specified object in its corresponding table.
        /// </summary>
        /// <param name="obj">The object to be deleted.</param>
        /// <returns>A Task object representing the asynchronous operation.</returns>
        public Task DeleteAsync(T obj);
    }
}
