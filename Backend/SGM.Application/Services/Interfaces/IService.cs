using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orion.Application.Services.Interfaces {
    /// <summary>
    /// An interface that specifies the basic CRUD operation methods for all services.
    /// </summary>
    /// <typeparam name="T">The concrete type handled by the implementing service.</typeparam>
    public interface IService<T> {
        /// <summary>
        /// Asynchronously saves the specified object in in the persistance layer, executing the necessary business logic.
        /// </summary>
        /// <param name="obj">The object to be saved.</param>
        /// <returns>A Task object representing the asynchronous operation.</returns>
        public Task CreateAsync(T obj);

        /// <summary>
        /// Asynchronously saves the specified collection of objects in in the persistance layer, executing the necessary business logic.
        /// </summary>
        /// <param name="objs">The collection of objects to be saved.</param>
        /// <returns>A Task object representing the asynchronous operation.</returns>
        public Task CreateAsync(IEnumerable<T> objs);

        /// <summary>
        /// Asynchronously retrieves the specified object from the persistance layer, executing the necessary business logic.
        /// </summary>
        /// <param name="ID">The unique identifier of the desired object.</param>
        /// <returns>A Task object representing the asynchronous operation.</returns>
        public Task<T> GetAsync(object ID);

        /// <summary>
        /// Asynchronously retrieves a collection of the specified type from the persistance layer, executing the necessary business logic.
        /// </summary>
        /// <returns>A Task object representing the asynchronous operation.</returns>
        public Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Asynchronously retrieves a collection of the specified type from the persistance layer, executing the necessary business logic.
        /// </summary>
        /// <param name="max">The maximum number of objects to be returned in the collection.</param>
        /// <returns>A Task object representing the asynchronous operation.</returns>
        public Task<IEnumerable<T>> GetAllAsync(int max);

        /// <summary>
        /// Asynchronously updates the specified object in the persistance layer, executing the necessary business logic.
        /// </summary>
        /// <param name="obj">The object to be updated.</param>
        /// <returns>A Task object representing the asynchronous operation.</returns>
        public Task UpdateAsync(T obj);

        /// <summary>
        /// Asynchronously deletes the specified object from the persistance layer, executing the necessary business logic.
        /// </summary>
        /// <param name="obj">The object to be deleted.</param>
        /// <returns>A Task object representing the asynchronous operation.</returns>
        public Task DeleteAsync(T obj);
    }
}
