using Lion.Core.Application._Common.Interfaces;
using Lion.Core.Application._Common.Models;
using Lion.Core.Domain._Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace Lion.Infrastructure.Persistence.Repositories._Common
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected DbContext Db;
        protected DbSet<TEntity> DbSet;
        private readonly IPublisher _mediator;
        public Repository(DbContext dbContext,
                          IPublisher mediator)
        {
            Db = dbContext;
            DbSet = Db.Set<TEntity>();
            _mediator = mediator;
        }

        public void Add(TEntity entity)
        {
            DbSet.Add(entity);
        }
        public void Update(TEntity entity)
        {
            DbSet.Update(entity);
        }
        public void Remove(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        public async Task<PagedList<TEntity>> Find(Expression<Func<TEntity, bool>> predicate,
                                                   BaseQueryParams baseQueryParams)
        {
            var items = DbSet.Where(predicate);
            items = items.OrderBy(baseQueryParams.OrderBy);

            return await PagedList<TEntity>.Factory.Create(items,
                                                           baseQueryParams.PageNumber,
                                                           baseQueryParams.PageSize);
        }

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.FirstOrDefaultAsync(predicate);
        }

        public async Task<int> Save()
        {
            var result = await Db.SaveChangesAsync();

            return result;
        }

    }
}
