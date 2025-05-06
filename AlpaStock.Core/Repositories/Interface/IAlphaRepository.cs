namespace AlpaStock.Core.Repositories.Interface
{
    public interface IAlphaRepository<TEntity>
    {
        Task<TEntity?> GetByIdAsync(string id);
        IQueryable<TEntity> GetQueryable();
        Task<TEntity> Add(TEntity entity);
        void Delete(List<TEntity> entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task AddRanges(List<TEntity> entity);
        Task<int> SaveChanges();
    }
}
