﻿using Lion.Core.Application._Common.Mappings;
using Lion.Core.Domain.Entities;

namespace Lion.Core.Application.Sellers.Commands.Add;
public class AddResult : IMapFrom<Seller>
{
    public string SellerId { get; init; }
    public string TaxId { get; init; }
    public string Name { get; init; }
}
