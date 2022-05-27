using Lion.Core.Application._Common.Models;
using Lion.Core.Domain._Common;
using System.Linq.Expressions;

namespace Lion.Core.Application._Common.Interfaces;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    void Add(TEntity entity);
    void Update(TEntity entity);
    Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate);
    Task<PagedList<TEntity>> Find(Expression<Func<TEntity, bool>> predicate, BaseQueryParams baseQueryParams);
    Task<int> Save();
}
