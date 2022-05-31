using Lion.Core.Application._Common.Interfaces.Repositories;
using Lion.Core.Domain.Entities;
using Lion.Infrastructure.Persistence.Context;
using Lion.Infrastructure.Persistence.Repositories._Common;
using MediatR;

namespace Lion.Infrastructure.Persistence.Repositories;

public class SellerRepository : Repository<Seller>, ISellerRepository
{
    public SellerRepository(LionDbContext dbContext,
                            IPublisher mediator) : base(dbContext, mediator)
    {

    }
}
