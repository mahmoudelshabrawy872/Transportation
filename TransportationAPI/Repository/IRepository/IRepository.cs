using System.Linq.Expressions;

namespace TransportationAPI.Repository.IRepository
{
    /// <summary>
    /// Generic repository interface that defines basic CRUD operations for a given entity type.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Retrieves all entities of type T from the database.
        /// </summary>
        /// <param name="filter">Optional filter expression to apply.</param>
        /// <returns>A task representing the asynchronous operation that returns a collection of entities.</returns>
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null);

        /// <summary>
        /// Retrieves a single entity of type T from the database.
        /// </summary>
        /// <param name="filter">Optional filter expression to apply.</param>
        /// <param name="tracked">Determines whether the entity should be tracked by the context.</param>
        /// <returns>A task representing the asynchronous operation that returns the first entity that matches the specified filter.</returns>
        Task<T> GetAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true);

        /// <summary>
        /// Creates a new entity in the database.
        /// </summary>
        /// <param name="entity">The entity to create.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task CreateAsync(T entity);

        /// <summary>
        /// Deletes an entity from the database.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task DeleteAsync(T entity);

        /// <summary>
        /// Saves changes made in the context to the database.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task SaveAsync();



    }
}
