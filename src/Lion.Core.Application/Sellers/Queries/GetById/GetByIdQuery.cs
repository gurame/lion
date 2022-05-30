using AutoMapper;
using Lion.Core.Application._Common.Exceptions;
using Lion.Core.Application._Common.Interfaces.Repositories;
using MediatR;

namespace Lion.Core.Application.Sellers.Queries.GetById;

public record GetByIdQuery : IRequest<GetByIdResult>
{
    public string SellerId { get; init; }
}

public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, GetByIdResult>
{
    private readonly ISellerRepository _repository;
    private readonly IMapper _mapper;
    public GetByIdQueryHandler(ISellerRepository repository,
                               IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetByIdResult> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.Get(x => x.SellerId == request.SellerId);
        if (entity == null)
        {
            throw new NotFoundException("Seller", request.SellerId);
        }

        return _mapper.Map<GetByIdResult>(entity);
    }
}