using AutoMapper;
using Lion.Core.Application._Common.Interfaces;
using Lion.Core.Application._Common.Interfaces.Repositories;
using Lion.Core.Domain.Entities;
using Lion.Core.Domain.Exceptions;
using MediatR;

namespace Lion.Core.Application.Sellers.Commands.Add;

public record AddCommand : IRequest<AddResult>
{
    public string TaxId { get; init; }
    public string Name { get; init; }
}

public class AddCommandHandler : IRequestHandler<AddCommand, AddResult>
{
    private readonly IUUID _uudi;
    private readonly ISellerRepository _repository;
    private readonly IMapper _mapper;
    public AddCommandHandler(IUUID uuid,
                             ISellerRepository repository,
                             IMapper mapper)
    {
        _uudi = uuid;
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<AddResult> Handle(AddCommand request, CancellationToken cancellationToken)
    {
        var exiting = await _repository.Get(x => x.TaxId == request.TaxId);
        if (exiting != null)
        {
            throw new DomainException($"Cannot add seller. Seller with tax id ({request.TaxId}) already exists.");
        }

        var @new = Seller.Factory.Create(_uudi.Id, request.TaxId, request.Name);

        _repository.Add(@new);
        await _repository.Save();

        return _mapper.Map<AddResult>(@new);
    }
}