namespace FogTracker.Data.Postgres.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Contracts.Repositories;
    using Microsoft.EntityFrameworkCore;

    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected FogContext FogContext { get; set; }
 
        public RepositoryBase(FogContext fogContext)
        {
            this.FogContext = fogContext;
        }
 
        public IQueryable<T> FindAll()
        {
            return this.FogContext.Set<T>().AsNoTracking();
        }
 
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.FogContext.Set<T>().Where(expression).AsNoTracking();
        }
 
        public void Create(T entity)
        {
            this.FogContext.Set<T>().Add(entity);
        }
 
        public void Update(T entity)
        {
            this.FogContext.Set<T>().Update(entity);
        }
 
        public void Delete(T entity)
        {
            this.FogContext.Set<T>().Remove(entity);
        }
    }
}