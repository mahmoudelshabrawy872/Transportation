using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TransportationAPI.Data;
using TransportationAPI.Repository.IRepository;

namespace TransportationAPI.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private protected ApplicationDbContext _context;
        internal DbSet<T> dbSet;

        /// <summary>
        /// Initializes a new instance of the Repository class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        public Repository(ApplicationDbContext context)
        {
            _context = context;
            this.dbSet = _context.Set<T>();
        }

        /// <summary>
        /// Gets all entities of type T from the database.
        /// </summary>
        /// <param name="filter">Optional filter expression to apply.</param>
        /// <returns>A collection of entities.</returns>
        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = dbSet;
            if (filter is not null)
                query = query.Where(filter);
            return query.ToList();
        }

        /// <summary>
        /// Gets a single entity of type T from the database.
        /// </summary>
        /// <param name="filter">Optional filter expression to apply.</param>
        /// <param name="tracked">Determines whether the entity should be tracked by the context.</param>
        /// <returns>The first entity that matches the specified filter.</returns>
        public async Task<T> GetAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true)
        {
            IQueryable<T> query = dbSet;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filter is not null)
                query = query.Where(filter);

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Creates a new entity in the database.
        /// </summary>
        /// <param name="entity">The entity to create.</param>
        public async Task CreateAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            await SaveAsync();
        }

        /// <summary>
        /// Deletes an entity from the database.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        public async Task DeleteAsync(T entity)
        {
            dbSet.Remove(entity);
            await SaveAsync();
        }

        /// <summary>
        /// Saves changes made in the context to the database.
        /// </summary>
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
