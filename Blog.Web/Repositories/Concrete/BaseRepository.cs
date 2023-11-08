using Blog.Web.Data;
using Blog.Web.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Blog.Web.Repositories.Concrete
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        public BlogDbContext dbContext { get; set; }

        public BaseRepository()
        {

            this.dbContext = new BlogDbContext();
        }

        #region Insert
        public virtual async Task<int> InsertAsync(T entity)
        {
            await dbContext.Set<T>().AddAsync(entity);
            return await dbContext.SaveChangesAsync();
        }
        #endregion

        #region Update
        public virtual async Task<int> UpdateAsync(T entity)
        {
            dbContext.Set<T>().Update(entity);
            return await dbContext.SaveChangesAsync();
        }
        #endregion

        #region Delete
        public async Task<int> DeleteAsync(T entity)
        {
            dbContext.Set<T>().Remove(entity);
            return await dbContext.SaveChangesAsync();
        }
        #endregion

        #region GetByIdAsync
        public async Task<T?> GetByIdAsync(string id)
        {
            return await dbContext.Set<T>().FindAsync(id);
        }
        #endregion

        #region GetBy
        public async Task<T?> GetBy(Expression<Func<T, bool>> filter)
        {
            return await dbContext.Set<T>().Where(filter).AsNoTracking().FirstOrDefaultAsync();
        }
        #endregion

        #region GetAllAsync
        public async Task<ICollection<T>> GetAllAsync()
        {
            return await dbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<ICollection<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null)
        {
            if (filter == null)
            {
                return await dbContext.Set<T>().ToListAsync();
            }
            return await dbContext.Set<T>().Where(filter).AsNoTracking().ToListAsync();
        }
        #endregion

        #region GetAllInclude
        public async Task<IQueryable<T>> GetAllInclude(Expression<Func<T, bool>>? filter = null, params Expression<Func<T, object>>[] include)
        {
            IQueryable<T> query;
            if (filter != null)
            {
                query = dbContext.Set<T>().Where(filter);
            }
            else
            {
                query = dbContext.Set<T>();
            }
            return include.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
        #endregion
    }
}
