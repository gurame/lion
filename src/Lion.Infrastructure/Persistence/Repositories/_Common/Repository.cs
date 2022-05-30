﻿using Lion.Core.Application._Common.Interfaces;
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
        private readonly IIdentityService _identityService;
        private readonly IDateTime _dateTime;
        public Repository(DbContext dbContext,
                          IMediator mediator,
                          IIdentityService identityService,
                          IDateTime dateTime)
        {
            Db = dbContext;
            DbSet = Db.Set<TEntity>();
            _mediator = mediator;
            _identityService = identityService;
            _dateTime = dateTime;
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
            foreach (var entry in this.Db.ChangeTracker.Entries<BaseAuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.SetCreatedInformation(_dateTime.Now,
                                                           _identityService.GetUserId());
                        break;
                    case EntityState.Modified:
                        entry.Entity.SetLastModifiedInformation(_dateTime.Now,
                                                                _identityService.GetUserId());
                        break;
                    default:
                        break;
                }
            }

            var result = await Db.SaveChangesAsync();

            return result;
        }

    }
}
