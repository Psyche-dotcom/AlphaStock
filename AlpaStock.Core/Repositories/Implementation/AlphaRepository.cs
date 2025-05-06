using AlpaStock.Core.Context;
using AlpaStock.Core.Repositories.Interface;




namespace AlpaStock.Core.Repositories.Implementation
{
    public class AlphaRepository<TEntity> : IAlphaRepository<TEntity> where TEntity : class
    {
        private readonly AlphaContext _context;

        public AlphaRepository(AlphaContext context)
        {
            _context = context;
        }
        public async Task<TEntity> Add(TEntity entity)
        {
            var result = await _context.Set<TEntity>().AddAsync(entity);
            return result.Entity;
        }   
        public async Task AddRanges(List<TEntity> entity)
        {
             await _context.Set<TEntity>().AddRangeAsync(entity);
            
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }
        public void Delete(List<TEntity> entity)
        {
            _context.Set<TEntity>().RemoveRange(entity);
        }
        public async Task<TEntity?> GetByIdAsync(string id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }


        public IQueryable<TEntity> GetQueryable()
        {
            return _context.Set<TEntity>();
        }

        public Task<int> SaveChanges()
        {
            return _context.SaveChangesAsync();
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }
    }
}
