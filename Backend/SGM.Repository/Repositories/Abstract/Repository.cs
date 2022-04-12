using System.Collections.Generic;
using Orion.Service.Repositories.Interfaces;
using Orion.Utilities.Dependency;
using Orion.Utilities.Logger.Interfaces;
using System;
using System.Threading.Tasks;

namespace Orion.Repository.Repositories.Abstract {
    /// <summary>
    /// A base abstract class that defines the basic elements for all repositories.
    /// This class is useful for defining and initializing elements that are share amongst all repositories (i.e. context, logger, etc).
    /// </summary>
    /// <typeparam name="T">The type handled by the implementing class.</typeparam>
    public abstract class Repository<T> : IRepository<T> where T: class {
        //protected readonly SomeDbContext _context; <-- If using EF Core.
        protected readonly ILoggerAsync _logger;

        public Repository() {
            // Inject necessary dependencies here, or use DependencyContainer to get them.
        }

        // Uncomment the abstract method below to force extending classes to define their own CRUD.
        //public abstract Task CreateAsync(T obj);
        //public abstract Task<T> GetAsync(object ID);
        //public abstract Task<IEnumerable<T>> GetAllAsync();
        //public abstract Task<IEnumerable<T>> GetAllAsync(int max);
        //public abstract Task<IEnumerable<T>> GetWithQueryAsync(FormattableString query);
        //public abstract Task UpdateAsync(T obj);
        //public abstract Task DeleteAsync(T obj);

        // Or use the methods below to define high level CRUD.
        public virtual async Task CreateAsync(T obj) {
            try {
                // If EF Core:
                //await this._context.Set<T>().AddAsync(obj);
                //await this._context.SaveChangesAsync();
                throw new NotImplementedException();
            }
            catch (Exception e) {
                await this._logger.LogAsync(e);
                throw e;
            }
        }

        public virtual async Task CreateAsync(IEnumerable<T> objs) {
            try {
                // If EF Core:
                //await this._context.Set<T>().AddRangeAsync(objs);
                //await this._context.SaveChangesAsync();
                throw new NotImplementedException();
            }
            catch (Exception e) {
                await this._logger.LogAsync(e);
                throw e;
            }
        }

        public virtual async Task<T> GetAsync(object ID) {
            try {
                // If EF Core:
                //return await this._context.Set<T>().FindAsync(ID);
                throw new NotImplementedException();
            }
            catch (Exception e) {
                await this._logger.LogAsync(e);
                throw e;
            }
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync() {
            try {
                // If EF Core:
                //return await this._context.Set<T>().AsNoTracking().ToListAsync();
                throw new NotImplementedException();
            }
            catch (Exception e) {
                await this._logger.LogAsync(e);
                throw e;
            }
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(int max) {
            try {
                // If EF Core:
                //return await this._context.Set<T>().AsNoTracking().Take(max).ToListAsync();
                throw new NotImplementedException();
            }
            catch (Exception e) {
                await this._logger.LogAsync(e);
                throw e;
            }
        }

        public virtual async Task<IEnumerable<T>> GetWithQueryAsync(FormattableString query) {
            try {
                // If EF Core:
                //return await this._context.Set<T>().FromSqlInterpolated(query).AsNoTracking().ToListAsync();
                throw new NotImplementedException();
            }
            catch (Exception e) {
                await this._logger.LogAsync(e);
                throw e;
            }
        }

        public virtual async Task UpdateAsync(T obj) {
            try {
                // If EF Core:
                //this._context.Set<T>().Update(obj);
                //await this._context.SaveChangesAsync();
                throw new NotImplementedException();
            }
            catch (Exception e) {
                await this._logger.LogAsync(e);
                throw e;
            }
        }

        public virtual async Task DeleteAsync(T obj) {
            try {
                // If EF Core:
                //this._context.Set<T>().Remove(obj);
                //await this._context.SaveChangesAsync();
                throw new NotImplementedException();
            }
            catch (Exception e) {
                await this._logger.LogAsync(e);
                throw e;
            }
        }

        protected virtual async Task ExecuteSQLAsync(string sql) {
            try {
                // If EF Core:
                //await this._context.Database.ExecuteSqlRawAsync(sql);

                throw new NotImplementedException();
            }
            catch (Exception e) {
                await this._logger.LogAsync(e);
                throw e;
            }
        }
    }
}
